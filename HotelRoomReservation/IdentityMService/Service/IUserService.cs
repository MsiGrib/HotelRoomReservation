using DataModel.DataBase;

namespace IdentityMService.Service
{
    public interface IUserService
    {
        public Task<UserDTO?> GetUserByIdAsync(string idGuid);
        public Task<UserDTO?> GetUserByEmailAsync(string email);
        public Task<bool> RegistrationUserAsync(string login, string password, string numberPhone, string lastName, string firstName, string email);
        public Task<UserDTO?> AuthorizationUserAsync(string login, string password);
    }
}
