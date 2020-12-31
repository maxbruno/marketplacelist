using MarketplaceList.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarketplaceList.Domain.Interfaces.Services
{
    public interface ITacoService
    {
        Task<List<Taco>> GetAllProductsTacoAsync();
    }
}
