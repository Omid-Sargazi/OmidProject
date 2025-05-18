using OmidProject.Frameworks.Utilities.Extensions;
using System.ComponentModel;
using System.Reflection;
using ClosedXML.Excel;

namespace OmidProject.Applications.Application.Reporting;

public class ExcelService
{
    public string GenerateExcelUsingObjectNames<T>(List<T> list, string sheetName = "")
    {
        var base64 = "";

        // ایجاد یک workbook جدید
        var workbook = new XLWorkbook();

        // ایجاد یک worksheet جدید با نام "Sheet1"
        var worksheet = workbook.Worksheets.Add("Sheet1");

        // دریافت هدرهای ستون‌ها از خود شئ
        var headers = typeof(T).GetProperties()
            .Select(p => p.Name)
            .ToList();

        // پر کردن سطر اول با هدرهای ستون‌ها
        for (var i = 0; i < headers.Count; i++) worksheet.Cell(1, i + 1).Value = headers[i];

        // پر کردن سطرهای داده‌ها با استفاده از لیست موجود
        for (var i = 0; i < list.Count; i++)
            for (var j = 0; j < headers.Count; j++)
            {
                var value = typeof(T).GetProperty(headers[j]).GetValue(list[i], null);
                if (value != null)
                    worksheet.Cell(i + 2, j + 1).Value = (XLCellValue)value.ToString();
            }

        // ذخیره فایل اکسل
        var stream = new MemoryStream();
        workbook.SaveAs(stream);
        base64 = Convert.ToBase64String(stream.ToArray());

        return base64;
    }

    public string GenerateExcelUsingClosedXML<T>(List<T> list, string sheetName = "")
    {
        var base64 = "";
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add($"{sheetName} {DateTime.Now.ToPersianDate("yyyy-MM-dd HH-mm-ss")}");

            var properties = GenericTypeExtensions.GetPropertiesWithDisplayName<T>()
                //.Where(w => w.GetValue(list[0]) != null)
                .ToList();

            // تعیین نام ستون‌ها
            for (var i = 0; i < properties.Count; i++)
                worksheet.Cell(1, i + 1).Value = properties[i].GetCustomAttribute<DisplayNameAttribute>()?.DisplayName;

            // ایجاد سطرهای داده‌ها
            var rowIndex = 2;
            foreach (var item in list)
            {
                for (var i = 0; i < properties.Count; i++)
                    worksheet.Cell(rowIndex, i + 1).Value = $"{properties[i].GetValue(item)}";
                rowIndex++;
            }

            // تنظیم سبک فایل Excel
            worksheet.Columns().AdjustToContents();
            worksheet.Columns().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.SetRightToLeft(true);
            workbook.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            workbook.Style.Font.Bold = true;

            // تولید فایل Excel و خروجی دادن آن به کاربر
            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            base64 = Convert.ToBase64String(stream.ToArray());
        }

        return base64;
    }
}