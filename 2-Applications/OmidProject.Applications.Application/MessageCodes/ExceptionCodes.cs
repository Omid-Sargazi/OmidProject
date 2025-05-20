namespace OmidProject.Applications.Application.MessageCodes;

public class ExceptionCodes
{
    public class Public
    {
        public static ApplicationExceptionMessageCode IdIsInvalid = 127000;
    }

    public class SystemMessage
    {
        public static ApplicationExceptionMessageCode SystemErrorCanNotFound = 20;
        public static ApplicationExceptionMessageCode SystemErrorCodeIsDuplicate = 21;
        public static ApplicationExceptionMessageCode SystemErrorMessageIsEmpty = 22;
    }

    public class Identity
    {
        public static ApplicationExceptionMessageCode IdentityError = 10;
        public static ApplicationExceptionMessageCode UsernameOrPasswordIncorrect = 11;
        public static ApplicationExceptionMessageCode ClientHaveNoToken = 12;
    }

    public class FormsAndRolesException
    {
        public static ApplicationExceptionMessageCode RouteIsNullOrEmpty = 1100;
        public static ApplicationExceptionMessageCode TitleIsNullOrEmpty = 1101;
        public static ApplicationExceptionMessageCode RoutIsAlreadyExist = 1102;
        public static ApplicationExceptionMessageCode AccessibleFormNotFound = 1103;
        public static ApplicationExceptionMessageCode RoleNotFound = 1104;
        public static ApplicationExceptionMessageCode RoleAccessibleFormAlreadyExist = 1105;
        public static ApplicationExceptionMessageCode RoleAccessibleFormNotFound = 1106;
        public static ApplicationExceptionMessageCode AccessibleFormIdsIsNullOrEmpty = 1107;
        public static ApplicationExceptionMessageCode RoleFrontPageFormAlreadyExist = 1108;
        public static ApplicationExceptionMessageCode FrontPageFormNotFound = 1109;
        public static ApplicationExceptionMessageCode FrontPageFormIdsIsNullOrEmpty = 1110;
        public static ApplicationExceptionMessageCode RoleFrontPageFormNotFound = 1111;
    }
    public class ProvinceException
    {
        public static ApplicationExceptionMessageCode ProvinceNameIsNullOrEmpty = 1200;
        public static ApplicationExceptionMessageCode ProvinceNameIsExist = 1201;
        public static ApplicationExceptionMessageCode ProvinceNotExist = 1202;
        public static ApplicationExceptionMessageCode ProvinceIdIsGreaterThan = 1203;
    }


}