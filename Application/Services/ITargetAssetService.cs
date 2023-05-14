using Domain;

namespace Application.Services
{
    public interface ITargetAssetService
    {
        Task<List<TargetAsset>> GetEnrichedTargetAssetsAsync();
    }
}
