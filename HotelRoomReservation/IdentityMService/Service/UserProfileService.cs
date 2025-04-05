using DataModel.DataBase;
using IdentityMService.EntityGateWay;
using System.Diagnostics.Metrics;

namespace IdentityMService.Service
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepository;

        public UserProfileService(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        public async Task<bool> PrimaryProfileConsciousnessAsync(UserDTO user)
        {
            var userProfile = new UserProfileDTO
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                User = user,
            };

            bool status = await _userProfileRepository.AddAsync(userProfile);

            return status;
        }

        public async Task<UserProfileDTO?> GetUserProfileByUserIdAsync(Guid userId)
        {
            return await _userProfileRepository.GetByUserIdAsync(userId);
        }
    }
}
