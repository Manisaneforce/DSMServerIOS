using DSMServerMani.Models;

namespace DSMServerMani.Services
{
    public interface ILoginService
    {
        Task<object> UserLoginVerification(LoginRequestModel loginRequestModel);
    }
}
