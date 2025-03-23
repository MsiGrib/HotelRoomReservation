using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.DataBase
{
    public class UserProfileDTO : IEntity<Guid>
    {
        public required Guid Id { get; set; }
        public string? ImagePatch { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }

        public required Guid UserId { get; set; }
        public virtual UserDTO User { get; set; }
    }
}
