using DataModel.DataBase;

namespace IdentityMService.Service
{
    internal class UserService : IUserService
    {
        private readonly IRepository<UserDTO, Guid> _userRepository;

        public UserService(IRepository<UserDTO, Guid> userRepository)
        {
            _userRepository = userRepository;
        }
    }
}
