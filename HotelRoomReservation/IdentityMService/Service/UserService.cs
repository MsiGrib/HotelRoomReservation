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

        public async Task<UserDTO?> GetUserByIdAsync(string idGuid)
        {
            if (string.IsNullOrEmpty(idGuid))
            {
                throw new Exception("Пустой id!");
            }

            if (!Guid.TryParse(idGuid, out var id))
            {
                throw new Exception("Не получилось спарсить Guid!");
            }

            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<UserDTO?> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new Exception("Пустой email!");
            }

            return await _userRepository.GetByEmailAsync(email);
        }

        public async Task AddUserAsync(string login, string password, string numberPhone, string lastName,
            string firstName, string email)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password)
                || string.IsNullOrEmpty(numberPhone) || string.IsNullOrEmpty(lastName)
                || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(email))
            {
                throw new Exception("Пустые данные!");
            }

            var user = new UserDTO
            {
                Id = Guid.NewGuid(),
                Login = login,
                Password = password,
                Email = email,
                NumberPhone = numberPhone,
                LastName = lastName,
                FirstName = firstName,
            };

            await _userRepository.AddAsync(user);
        }
    }
}
