using Domain;

namespace Infrastructure.TargetAssetRepo
{
    public interface ITargetAssetRepository
    {
        Task<List<TargetAsset>> GetTargetAssetsAsync();
    }
}
