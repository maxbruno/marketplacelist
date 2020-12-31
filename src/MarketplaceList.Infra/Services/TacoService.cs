using MarketplaceList.Domain.Interfaces.Services;
using MarketplaceList.Domain.Services;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MarketplaceList.Infra.Services
{
    public class TacoService : ITacoService
    {

        private readonly HttpClient _httpClient;

        public TacoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Taco>> GetAllProductsTacoAsync()
        {
            var response = await _httpClient.GetAsync("/api/v1/food");
            var stringResponse = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Taco>>(stringResponse);
        }
    }
}
