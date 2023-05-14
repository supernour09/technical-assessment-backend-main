using Domain;
using System.Text.Json;

namespace Infrastructure.TargetAssetRepo
{
    public class TargetAssetRepository : ITargetAssetRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public TargetAssetRepository(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<List<TargetAsset>> GetTargetAssetsAsync()
        {
            var client = _clientFactory.CreateClient("TargetAssetAPI");
            var response = await client.GetAsync("/targetAsset");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var targetAssets = JsonSerializer.Deserialize<List<TargetAsset>>(json);
            return targetAssets;
        }
    }

}
