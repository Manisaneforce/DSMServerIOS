using DSMServerMani.Models;

namespace DSMServerMani.Repositories
{
    public interface ICheckinRepository
    {
        Task<object> UserCheckinVerification(CheckinRequestModel checkinRequestModel);
    }
}
