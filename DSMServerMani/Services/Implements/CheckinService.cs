using DSMServerMani.Models;
using DSMServerMani.Repositories;

namespace DSMServerMani.Services.Implements
{
    public class CheckinService : ICheckinService
    {
        private readonly ICheckinRepository _repository;

        public CheckinService(ICheckinRepository repository)
        {
            _repository = repository;
        }

        public async Task<object> UserCheckinVerification(CheckinRequestModel checkinRequestModel)
        {
            return await _repository.UserCheckinVerification(checkinRequestModel);
        }
    }
}
