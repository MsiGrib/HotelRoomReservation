using DataModel.DataBase;

namespace IdentityMService.Service
{
    public interface IUserProfileService
    {
        public Task<bool> CreateUserProfileAsync(UserDTO user, string imagePatch, string country, string city, string postalCode);
        public Task<bool> PrimaryProfileConsciousness(UserDTO user);
    }
}
