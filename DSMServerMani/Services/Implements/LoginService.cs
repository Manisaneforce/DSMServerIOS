using DSMServerMani.Models;
using DSMServerMani.Repositories.Implements;

namespace DSMServerMani.Services.Implements
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _repository;

        public LoginService(ILoginRepository repository)
        {
            _repository = repository;
        }
        public async Task<object> UserLoginVerification(LoginRequestModel loginRequestModel)
        {
            return await _repository.UserLoginVerification(loginRequestModel);
        }
    }
}
