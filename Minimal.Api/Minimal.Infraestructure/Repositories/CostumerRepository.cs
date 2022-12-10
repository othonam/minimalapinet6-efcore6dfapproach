using Microsoft.EntityFrameworkCore;
using Minimal.Domain.Context;
using Minimal.Domain.Entities;
using Minimal.Infraestructure.Context;

namespace Minimal.Infrastructure.Repositories
{
    public class CostumerRepository : ICostumerRepository
    {
        private readonly AppDbContext _dbContext;
        public CostumerRepository(AppDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> PostCostumer(Costumer costumer)
        {
            var tracking = _dbContext.Costumers.Add(costumer);
            await _dbContext.SaveChangesAsync();

            return tracking.Entity.Id;
        }

        public async Task<IEnumerable<Costumer>> GetCostumers()
        {
            return await _dbContext.Costumers.ToListAsync();
        }
    }
}
