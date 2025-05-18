using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace OmidProject.Host;

public class Program
{
    public static void Main(string[] args)
    {
        // ذخیره مسیر پروژه در appsetting
        UpdateProjectPathSettings("appsettings.Development.json", Directory.GetCurrentDirectory());
        UpdateProjectPathSettings("appsettings.json", Directory.GetCurrentDirectory());
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(app =>
            {
                var settingFolder = Directory.GetParent(typeof(Program).Assembly.Location)?.FullName;
                //var settingFolder = Path.Combine(env, "Appsettings");
                Console.WriteLine(settingFolder);
                app.AddJsonFile(Path.Combine(settingFolder, "appsettings.json"), false, true);
                app.AddJsonFile(Path.Combine(settingFolder, "appsettings.Development.json"), false, true);
                app.AddJsonFile("defaultData.json", false, true);
            })
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
            .UseSerilog();
    }

    //private static void UpdateProjectPathSettings(string configFileName, string folderPath)
    //{
    //    // مسیر کامل فایل appsettings
    //    var configFilePath = Path.Combine(folderPath, configFileName);

    //    // بررسی وجود فایل
    //    if (!File.Exists(configFilePath))
    //    {
    //        Console.WriteLine($"{configFileName} not found in {folderPath}.");
    //        return;
    //    }

    //    // خواندن محتوای فایل appsettings
    //    var json = File.ReadAllText(configFilePath);

    //    // پارس کردن محتوای JSON به دیکشنری
    //    var jsonDocument = JsonDocument.Parse(json);
    //    var jsonObject = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonDocument.RootElement.GetRawText());

    //    // بررسی یا ایجاد ProjectPathSettings در فایل JSON
    //    if (!jsonObject.ContainsKey("ProjectPathSettings") ||
    //        jsonObject["ProjectPathSettings"] is not List<object> existingList)
    //        jsonObject["ProjectPathSettings"] = new List<object>();

    //    // اضافه کردن مسیر پروژه به ProjectPathSettings
    //    var projectPathEntry = new Dictionary<string, string>
    //    {
    //        { "ProjectPath", Directory.GetCurrentDirectory() }
    //    };

    //    ((List<object>)jsonObject["ProjectPathSettings"]).Add(projectPathEntry);

    //    // ذخیره تغییرات در فایل appsettings
    //    var updatedJson = JsonSerializer.Serialize(jsonObject, new JsonSerializerOptions { WriteIndented = true });
    //    File.WriteAllText(configFilePath, updatedJson);

    //    Console.WriteLine($"ProjectPath updated in {configFilePath}");
    //}

    private static void UpdateProjectPathSettings(string configFileName, string folderPath)
    {
        // مسیر کامل فایل appsettings
        var configFilePath = Path.Combine(folderPath, configFileName);

        // بررسی وجود فایل
        if (!File.Exists(configFilePath))
        {
            Console.WriteLine($"{configFileName} not found in {folderPath}.");
            return;
        }

        // خواندن محتوای فایل appsettings
        var json = File.ReadAllText(configFilePath);

        // پارس کردن JSON به یک دیکشنری
        var jsonDocument = JsonDocument.Parse(json);
        var jsonObject = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonDocument.RootElement.GetRawText());

        // اگر ProjectPathSettings وجود ندارد، آن را ایجاد کن
        if (!jsonObject.ContainsKey("ProjectPathSettings") || jsonObject["ProjectPathSettings"] is not Dictionary<string, object> projectPathSettings)
        {
            projectPathSettings = new Dictionary<string, object>();
            jsonObject["ProjectPathSettings"] = projectPathSettings;
        }

        // به‌روزرسانی ProjectPath
        projectPathSettings["ProjectPath"] = Directory.GetCurrentDirectory();

        // ذخیره تغییرات در فایل appsettings
        var updatedJson = JsonSerializer.Serialize(jsonObject, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(configFilePath, updatedJson);

        Console.WriteLine($"ProjectPath updated in {configFilePath}");
    }
}