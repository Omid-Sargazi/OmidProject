# مسیر جستجو را تنظیم کنید (مثال: $path = "C:\Projects")
$path = "C:\Users\amirc\source\repos\"
$oldName = "OmidProject"
$newName = "YOUR-PROJECT-NAME"

# ابتدا فولدرها را از عمیق‌ترین به سطحی مرتب کنید
Get-ChildItem -Path $path -Recurse -Directory | 
Where-Object { $_.Name -like "*$oldName*" } |
Sort-Object -Property { $_.FullName.Length } -Descending | 
Rename-Item -NewName { $_.Name -replace $oldName, $newName }

# سپس فایل‌ها را تغییر نام دهید
Get-ChildItem -Path $path -Recurse -File | 
Where-Object { $_.Name -like "*$oldName*" } |
Rename-Item -NewName { $_.Name -replace $oldName, $newName }