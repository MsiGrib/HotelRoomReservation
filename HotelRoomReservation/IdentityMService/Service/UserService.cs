using DataModel.DataBase;
using IdentityMService.EntityGateWay;
using IdentityMService.Utilitys;
using Microsoft.AspNetCore.Identity;
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

        public async Task<bool> RegistrationUserAsync(string login, string password, string numberPhone, string lastName,
            string firstName, string email)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password)
                || string.IsNullOrEmpty(numberPhone) || string.IsNullOrEmpty(lastName)
                || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(email))
            {
                throw new Exception("Пустые данные!");
            }

            var users = await _userRepository.GetAllAsync();

            bool isExists = users.Any(x => x.Login == login && x.Email == email);

            if (isExists)
            {
                throw new Exception("Такой пользователь уже есть!");
            }

            var user = new UserDTO
            {
                Id = Guid.NewGuid(),
                Login = login,
                Password = HashUtility.HashPassword(password),
                Email = email,
                NumberPhone = numberPhone,
                LastName = lastName,
                FirstName = firstName,
            };

            bool status = await _userRepository.AddAsync(user);

            return status;
        }

        public async Task<UserDTO?> AuthorizationUserAsync(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                throw new Exception("Пустые данные!");
            }

            var user = await _userRepository.GetByKredsAsync(login);

            if (user == null)
            {
                return null;
            }

            if (!HashUtility.VerifyPassword(password, user.Password))
            {
                return null;
            }

            return user;
        }
    }
}
