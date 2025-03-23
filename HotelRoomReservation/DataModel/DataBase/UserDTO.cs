using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.DataBase
{
    public class UserDTO : IEntity<Guid>
    {
        public required Guid Id { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public required string NumberPhone { get; set; }
        public required string LastName { get; set; }
        public required string FirstName { get; set; }
        public required DateTime Birthday { get; set; }

        public virtual UserProfileDTO UserProfile { get; set; }
    }
}
