using DSMServerMani.Models;

namespace DSMServerMani.Services
{
    public interface ICheckinService
    {
        Task<object> UserCheckinVerification(CheckinRequestModel checkinRequestModel);
    }
}
