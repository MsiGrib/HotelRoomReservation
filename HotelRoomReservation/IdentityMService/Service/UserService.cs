using DataModel.DataBase;
using IdentityMService.EntityGateWay;
using System.Threading.Tasks;

namespace IdentityMService.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO?> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new Exception("Пустой email!");
            }

            return await _userRepository.GetByEmailAsync(email);
        }
    }
}
