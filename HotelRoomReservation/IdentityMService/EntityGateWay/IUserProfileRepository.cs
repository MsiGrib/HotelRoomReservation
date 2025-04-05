using DataModel.DataBase;

namespace IdentityMService.EntityGateWay
{
    public interface IUserProfileRepository : IRepository<UserProfileDTO, Guid>
    {
        public Task<UserProfileDTO?> GetByUserIdAsync(Guid id);
    }
}
