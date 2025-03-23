using DataModel.DataBase;

namespace IdentityMService.EntityGateWay
{
    public interface IUserProfileRepository : IRepository<UserProfileDTO, Guid>
    {
    }
}
