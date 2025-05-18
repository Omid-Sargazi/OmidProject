using System.Reflection;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Applications.Contracts.Repository.Identity;
using OmidProject.Applications.Contracts.Service;
using OmidProject.Domains.Domain.General;
using OmidProject.Domains.Domain.Identity;
using OmidProject.Frameworks.Utilities.StaticData;
using OmidProject.Infrastructures.CommandDb;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;

namespace OmidProject.Infrastructures.Configurations.DefaultData;

public class DefaultDataInitializer
{
    private readonly IAccessibleFormRepository _accessibleFormRepository;
    private readonly OmidProjectCommandDb _dbContext;
    private readonly IMemoryCacheService _memoryCacheService;
    private readonly IRoleAccessibleFormRepository _roleAccessibleFormRepository;
    private readonly IApplicationUserRepository _applicationUserRepository;
    private readonly IApplicationRoleRepository _applicationRoleRepository;

    public DefaultDataInitializer(IAccessibleFormRepository accessibleFormRepository, OmidProjectCommandDb dbContext, IMemoryCacheService memoryCacheService, IRoleAccessibleFormRepository roleAccessibleFormRepository, IApplicationUserRepository applicationUserRepository, IApplicationRoleRepository applicationRoleRepository)
    {
        _accessibleFormRepository = accessibleFormRepository;
        _dbContext = dbContext;
        _memoryCacheService = memoryCacheService;
        _roleAccessibleFormRepository = roleAccessibleFormRepository;
        _applicationUserRepository = applicationUserRepository;
        _applicationRoleRepository = applicationRoleRepository;
    }

    public async Task Execute(Assembly assembly)
    {
        await AddRoles();
        await AddUsers();
        await AddUsersToRoles();
        await AddAccessibleForms(assembly);
        await SetAccessibleFormsForApplicantRole();
        //await AddTimeUnits();
        await ConfigMemoryCache();
    }

    private async Task ConfigMemoryCache()
    {
        var applicantRole = _dbContext.Roles
            .Include(x => x.RoleAccessibleForms)
            .ThenInclude(x => x.AccessibleForm);

        _memoryCacheService.SetObject("applicantRole", applicantRole);
    }

    //private async Task AddTimeUnits()
    //{
    //    if (!await _timeUnitRepository.IsExist("ثانیه")) await _timeUnitRepository.AddAsync(new TimeUnit("ثانیه", 1));
    //    if (!await _timeUnitRepository.IsExist("دقیقه")) await _timeUnitRepository.AddAsync(new TimeUnit("دقیقه", 60));
    //    if (!await _timeUnitRepository.IsExist("ساعت"))
    //        await _timeUnitRepository.AddAsync(new TimeUnit("ساعت", 60 * 60));
    //    if (!await _timeUnitRepository.IsExist("روز"))
    //        await _timeUnitRepository.AddAsync(new TimeUnit("روز", 60 * 60 * 24));
    //    if (!await _timeUnitRepository.IsExist("هفته"))
    //        await _timeUnitRepository.AddAsync(new TimeUnit("هفته", 60 * 60 * 24 * 7));
    //    if (!await _timeUnitRepository.IsExist("ماه"))
    //        await _timeUnitRepository.AddAsync(new TimeUnit("ماه", 60 * 60 * 24 * 7 * 30));
    //}

    private async Task AddUsers()
    {
        //if (await _userManager.Users.FirstOrDefaultAsync(x => x.Id == GuidStaticData.GetAmirCliperUserId()) == null)
        //{
        //    var user = new ApplicantUser();
        //    user.Email = "amircliper@gmail.com";
        //    user.PhoneNumber = "+989128136797";
        //    user.EmailConfirmed = true;
        //    user.PhoneNumberConfirmed = true;
        //    user.UserName = "amircliper";
        //    user.Id = new Guid("af01b865-30ef-4da7-bc68-273dd22f42e9");

        //    await _userManager.CreateAsync(user, "M@vie1381");
        //}

        if (await _applicationUserRepository.FindByIdAsync(GuidStaticData.GetPublicUserId().ToString()) == null)
        {
            var user = new ApplicationUser();
            user.Email = "publilc@gmail.com";
            user.PhoneNumber = "+989128136797";
            user.EmailConfirmed = true;
            user.PhoneNumberConfirmed = true;
            user.UserName = "public";
            user.Id = new Guid("C6D86FF5-C109-4948-B759-FE8F6ACD87EB");
            user.CreatedAt = DateTime.Now;
            user.FirstName = "public";
            user.LastName = "public";

            await _applicationUserRepository.CreateAsync(user, "M@vie1381");
        }

        if (await _applicationUserRepository.FindByIdAsync(GuidStaticData.GetSuperAdminUserId().ToString()) == null)
        {
            var user = new ApplicationUser();
            user.Email = "SuperAdmin@gmail.com";
            user.PhoneNumber = "+989121234567";
            user.EmailConfirmed = true;
            user.PhoneNumberConfirmed = true;
            user.UserName = "SuperAdmin";
            user.IsSuperAdmin = true;
            user.Id = new Guid("46bd25b8-8c32-4575-8afa-69c475683e60");
            user.CreatedAt = DateTime.Now;
            user.FirstName = "SuperAdmin";
            user.LastName = "SuperAdmin";

            await _applicationUserRepository.CreateAsync(user, "Aa123456");
            await _applicationUserRepository.AddToRoleAsync(user, "SuperAdmin");
        }
    }

    private async Task AddRoles()
    {
        if (await _applicationRoleRepository.FindByIdAsync(GuidStaticData.GetRootRoleId().ToString()) == null)
        {
            var rootRole = new ApplicationRole();
            rootRole.Id = new Guid("450517d6-d185-4a72-9778-8705c8f8370c");
            rootRole.Name = "root";
            rootRole.NormalizedName = "ROOT";
            rootRole.ConcurrencyStamp = "52a50c1f-002e-4bc2-b420-da9a0c8db456";

            await _applicationRoleRepository.CreateAsync(rootRole);
        }

        if (await _applicationRoleRepository.FindByIdAsync(GuidStaticData.GetPublicRoleId().ToString()) == null)
        {
            var rootRole = new ApplicationRole();
            rootRole.Id = new Guid("485CA2CE-1A0E-4B84-BF4C-289BCBC34331");
            rootRole.Name = "public";
            rootRole.NormalizedName = "PUBLIC";
            rootRole.ConcurrencyStamp = "51724890A-34FA-440E-98F6-1968E99E9769";

            await _applicationRoleRepository.CreateAsync(rootRole);
        }

        if (await _applicationRoleRepository.FindByIdAsync(GuidStaticData.GetSuperAdminRoleId().ToString()) == null)
        {
            var rootRole = new ApplicationRole();
            rootRole.Id = new Guid("db071ef2-7026-4876-b8fb-c45a174fe736");
            rootRole.Name = "SuperAdmin";
            rootRole.NormalizedName = "SUPERADMIN";
            rootRole.ConcurrencyStamp = "3fd79d4d-28b1-436a-8c55-8c9b71952441";

            await _applicationRoleRepository.CreateAsync(rootRole);
        }

        if (await _applicationRoleRepository.FindByIdAsync(GuidStaticData.GetApplicantRoleId().ToString()) == null)
        {
            var rootRole = new ApplicationRole();
            rootRole.Id = new Guid("46ecc556-0278-4c0a-7c7a-08dc0e9ab0b0");
            rootRole.Name = "Applicant";
            rootRole.NormalizedName = "APPLICANT";
            rootRole.ConcurrencyStamp = "9c19e361-f5a5-4772-96ba-1d999539b586";

            await _applicationRoleRepository.CreateAsync(rootRole);
        }
    }

    private async Task AddUsersToRoles()
    {
        //var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == "amircliper");
        //var publicUser = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == "public");
        var administrator = await _applicationUserRepository.FindByNameAsync("SuperAdmin");
        var superAdmin = await _applicationUserRepository.FindByNameAsync("SuperAdmin");

        //if (user != null) await _userManager.AddToRoleAsync(user, "root");
        //if (publicUser != null) await _userManager.AddToRoleAsync(publicUser, "public");
        if (superAdmin != null) await _applicationUserRepository.AddToRoleAsync(superAdmin, "SuperAdmin");
    }

    private async Task AddAccessibleForms(Assembly assembly)
    {
        var apiMethods = new Dictionary<string, string>();
        var fullRouteSet = new HashSet<string>();

        // دریافت همه‌ی انجام‌دهندگان (controllers) در اپلیکیشن
        var controllers = assembly.GetTypes()
            .Where(type => typeof(ControllerBase).IsAssignableFrom(type) && !type.IsAbstract);

        foreach (var controllerType in controllers)
        {
            var methods = controllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(method => method.GetCustomAttributes(typeof(HttpMethodAttribute), true).Any());

            foreach (var methodInfo in methods)
            {
                // دریافت RouteAttribute برای هر متد
                var routeAttributes = methodInfo.GetCustomAttributes<HttpMethodAttribute>();
                //var routeAttribute = methodInfo.GetCustomAttribute<RouteAttribute>();
                //var actionDescriptor = Attribute.GetCustomAttribute(methodInfo, typeof(ControllerActionDescriptor));

                if (routeAttributes != null && routeAttributes.Any())
                {
                    var controllerName = controllerType.Name.Replace("Controller", "");
                    //if (controllerName == "ProvincialExpertInspection")
                    //    Console.WriteLine();
                    var actionRoute = routeAttributes.FirstOrDefault()?.Template ?? "";
                    var methodName = methodInfo.Name;

                    // اگر متد ApiControllerAttribute داشته باشد، از اسم کلاس به جای نام کنترلر استفاده کنید
                    //var apiControllerAttribute = controllerType.GetCustomAttribute<ApiControllerAttribute>();
                    //if (apiControllerAttribute != null) controllerName = controllerType.Name;

                    // ایجاد مسیر کامل
                    var fullRoute = $"/{controllerName}/{actionRoute}";

                    // بررسی تکرار نام متد
                    if (!fullRouteSet.Add(fullRoute)) throw new Exception($"Duplicate api name found: {fullRoute}");

                    // اضافه کردن به دیکشنری
                    apiMethods[fullRoute] = methodName;
                }
            }
        }

        var accessibleFormList = new List<AccessibleForm>(
            apiMethods.Select(s => new AccessibleForm(s.Value, s.Key))
        );

        var dbAccessibleForms = await _accessibleFormRepository.GetAllAsync();
        var dbAccessibleFormsRoute = dbAccessibleForms.Select(s => s.Route).ToList();

        accessibleFormList = accessibleFormList.Where(w => !dbAccessibleFormsRoute.Contains(w.Route)).ToList();
        _accessibleFormRepository.AddRange(accessibleFormList);
    }

    private async Task SetAccessibleFormsForApplicantRole()
    {
        var role = await _applicationRoleRepository.FindByNameAsync("Applicant");

        if (role != null)
        {
            var roleAccessibleForms = await _roleAccessibleFormRepository.GetWithRoleId(role.Id);

            var accessibleForms = await _accessibleFormRepository.GetAllAsync();

            var unusedAccessibleForms = accessibleForms
                .Where(w => !roleAccessibleForms.Select(raf => raf.AccessibleFormId).Contains(w.Id)).ToList();

            var newRoleAccessibleForms =
                unusedAccessibleForms.Select(s => new RoleAccessibleForm(role.Id, s.Id)).ToList();

            _roleAccessibleFormRepository.AddRange(newRoleAccessibleForms);
        }
    }
}