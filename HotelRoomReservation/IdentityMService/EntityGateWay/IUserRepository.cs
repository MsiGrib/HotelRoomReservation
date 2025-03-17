using DataModel.DataBase;

namespace IdentityMService.EntityGateWay
{
    public interface IUserRepository : IRepository<UserDTO, Guid>
    {
        public Task<UserDTO?> GetByEmailAsync(string email);
    }
}
