

using DSMServerMani.Models;

namespace DSMServerMani.Repositories.Implements
{
    public interface ILoginRepository
    {
        Task<object> UserLoginVerification(LoginRequestModel loginRequestModel);
    }
}
