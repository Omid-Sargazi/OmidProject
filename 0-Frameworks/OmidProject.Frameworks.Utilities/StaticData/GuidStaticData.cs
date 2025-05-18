namespace OmidProject.Frameworks.Utilities.StaticData;

public static class GuidStaticData
{
    public static Guid GetAmirCliperUserId()
    {
        return Guid.Parse("af01b865-30ef-4da7-bc68-273dd22f42e9");
    }

    public static Guid GetPublicUserId()
    {
        return Guid.Parse("C6D86FF5-C109-4948-B759-FE8F6ACD87EB");
    }

    public static Guid GetSuperAdminUserId()
    {
        return Guid.Parse("46bd25b8-8c32-4575-8afa-69c475683e60");
    }

    public static Guid GetRootRoleId()
    {
        return Guid.Parse("450517d6-d185-4a72-9778-8705c8f8370c");
    }

    public static Guid GetPublicRoleId()
    {
        return Guid.Parse("485CA2CE-1A0E-4B84-BF4C-289BCBC34331");
    }

    public static Guid GetSuperAdminRoleId()
    {
        return Guid.Parse("db071ef2-7026-4876-b8fb-c45a174fe736");
    }

    public static Guid GetApplicantRoleId()
    {
        return Guid.Parse("46ecc556-0278-4c0a-7c7a-08dc0e9ab0b0");
    }
}