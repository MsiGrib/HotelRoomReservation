using DataModel.DataBase;

namespace IdentityMService.Service
{
    public interface IUserService
    {
        public Task<UserDTO?> GetUserByEmailAsync(string email);
    }
}
