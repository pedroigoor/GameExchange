using GameExchange.Domain.Entities;
using GameExchange.Domain.Repositories.Platform;
using Microsoft.EntityFrameworkCore;

namespace GameExchange.Infrastructe.DataAccess.Repositories
{
    public class PlatformRepository(GameExchangeDbContext context) : IPlatformWriteOnlyRepository , IPlatformUpdateOnlyRepository, IPlatformReadOnlyRepository
    {
        private readonly GameExchangeDbContext _context = context;


        public async Task Add(Platform platform)
        {
          await  _context.Platforms.AddAsync(platform);
        }

        async Task<Platform?> IPlatformUpdateOnlyRepository.GetById(long id)
        {
            return await _context.Platforms.FirstOrDefaultAsync(p => p.Id == id);
        }
        async Task<Platform?> IPlatformReadOnlyRepository.GetById(long id)
        {
            return await _context.Platforms.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public void Update(Platform platform) => _context.Platforms.Update(platform);

        public async Task<List<Platform>> GetAll()
        {
            return await _context.Platforms.AsNoTracking().ToListAsync();
        }
    }
}
