using DataModel.DataBase;
using Microsoft.EntityFrameworkCore;

namespace IdentityMService.EntityGateWay
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityDBContext _context;

        public UserRepository(IdentityDBContext context)
        {
            _context = context;
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<UserDTO?> GetByIdAsync(Guid id)
        {
            return await _context.Users.AsNoTracking().Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<UserDTO?> GetByEmailAsync(string email)
        {
            return await _context.Users.AsNoTracking().Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task AddAsync(UserDTO entity)
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserDTO entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var user = await GetByIdAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
