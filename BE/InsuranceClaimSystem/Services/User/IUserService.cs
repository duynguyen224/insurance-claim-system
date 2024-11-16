namespace InsuranceClaimSystem.Services.User
{
    public interface IUserService
    {
        string GetUserId();
        IEnumerable<string> GetRoles();
    }
}
