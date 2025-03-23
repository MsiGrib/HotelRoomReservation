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

        public async Task<bool> PrimaryProfileConsciousness(UserDTO user)
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

        public async Task<bool> CreateUserProfileAsync(UserDTO user, string imagePatch, string country, string city, string postalCode)
        {
            var userProfile = new UserProfileDTO
            {
                Id = Guid.NewGuid(),
                ImagePatch = imagePatch,
                Country = country,
                City = city,
                PostalCode = postalCode,
                UserId = user.Id,
                User = user,
            };

            bool status = await _userProfileRepository.AddAsync(userProfile);

            return status;
        }
    }
}
