using DataModel.DataBase;

namespace IdentityMService.Service
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserDTO, Guid> _userRepository;

        public UserService(IRepository<UserDTO, Guid> userRepository)
        {
            _userRepository = userRepository;
        }

        public UserDTO? GetUserBy
    }
}
