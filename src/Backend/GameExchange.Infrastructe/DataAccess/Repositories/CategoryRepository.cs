using GameExchange.Domain.Entities;
using GameExchange.Domain.Repositories.Category;
using Microsoft.EntityFrameworkCore;

namespace GameExchange.Infrastructe.DataAccess.Repositories
{
    public class CategoryRepository(GameExchangeDbContext context) : ICategoryReadOnlyRepository, ICategoryUpdateOnlyRepository,ICategoryWriteOnlyRepository
    {
        private readonly GameExchangeDbContext _context = context;

        public async Task Add(Category category) => await _context.AddAsync(category);

        public async Task<List<Category>> GetAll() => await _context.Categories.AsNoTracking().ToListAsync();       

        async Task<Category?> ICategoryReadOnlyRepository.GetById(long id) =>  await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

        async Task<Category?> ICategoryUpdateOnlyRepository.GetById(long id) => await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
       

        public void Update(Category category) => _context.Categories.Update(category);
    }
}
