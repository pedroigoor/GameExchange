using GameExchange.Domain.Entities;
using GameExchange.Domain.Repositories.Game;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameExchange.Infrastructe.DataAccess.Repositories
{
    public class GameRepository(GameExchangeDbContext context) : IGameReadOnlyRepository, IGameUpdateOnlyRepository, IGameWriteOnlyRepository
    {
        private readonly GameExchangeDbContext _context = context;

        public async Task Add(Game Game) => await _context.AddAsync(Game);

        public async Task<List<Game>> GetAll() {
            return await _context
                         .Games
                         .AsNoTracking()
                         .Include(g => g.Category)
                         .Include(g => g.Platform)
                         .ToListAsync();
         }

        async Task<Game?> IGameReadOnlyRepository.GetById(long id) => await _context.Games.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

        async Task<Game?> IGameUpdateOnlyRepository.GetById(long id) => await _context.Games.FirstOrDefaultAsync(c => c.Id == id);


        public void Update(Game Game) => _context.Games.Update(Game);
    }
}
