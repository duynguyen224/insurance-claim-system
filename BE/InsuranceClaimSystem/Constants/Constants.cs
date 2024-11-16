namespace InsuranceClaimSystem.Constants
{
    public class Roles
    {
        public const string ROLE_ADMIN = "admin";
        public const string ROLE_USER = "user";

    }

    public enum HttpStatus
    {
        Ok = 200,
        Created = 201,
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        InternalServerError = 500,
    }
}
