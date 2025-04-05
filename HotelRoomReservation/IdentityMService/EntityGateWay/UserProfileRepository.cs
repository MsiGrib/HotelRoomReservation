using DataModel.DataBase;
using System.Data.Entity;

namespace IdentityMService.EntityGateWay
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly IdentityDBContext _context;

        public UserProfileRepository(IdentityDBContext context)
        {
            _context = context;
        }

        public async Task<List<UserProfileDTO>> GetAllAsync()
        {
            return await _context.UsersProfile
                .Include(x => x.User)
                .ToListAsync();
        }

        public async Task<UserProfileDTO?> GetByIdAsync(Guid id)
        {
            return await _context.UsersProfile
                .AsNoTracking()
                .Include(x => x.User)
                .Where(up => up.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<UserProfileDTO?> GetByUserIdAsync(Guid id)
        {
            return await _context.UsersProfile
                .AsNoTracking()
                .Include(x => x.User)
                .Where(up => up.UserId == id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> AddAsync(UserProfileDTO entity)
        {
            await _context.UsersProfile.AddAsync(entity);
            int result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> UpdateAsync(UserProfileDTO entity)
        {
            _context.Update(entity);
            int result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var userProfile = await GetByIdAsync(id);
            if (userProfile != null)
            {
                _context.UsersProfile.Remove(userProfile);
                int result = await _context.SaveChangesAsync();

                return result > 0;
            }

            return false;
        }
    }
}
