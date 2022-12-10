using Minimal.Domain.Entities;

namespace Minimal.Domain.Context
{
    public interface ICostumerRepository
    {

        public Task<IEnumerable<Costumer>> GetCostumers();

        public Task<Guid> PostCostumer(Costumer costumer);
    }
}
