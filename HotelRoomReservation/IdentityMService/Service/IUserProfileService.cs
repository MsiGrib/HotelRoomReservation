using DataModel.DataBase;

namespace IdentityMService.Service
{
    public interface IUserProfileService
    {
        public Task<bool> PrimaryProfileConsciousnessAsync(UserDTO user);
        public Task<UserProfileDTO?> GetUserProfileByUserIdAsync(Guid userId);
    }
}
