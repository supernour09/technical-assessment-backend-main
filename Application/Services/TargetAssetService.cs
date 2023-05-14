using Domain;
using Infrastructure.IDateTimeService;
using Infrastructure.TargetAssetRepo;
using System;

namespace Application.Services
{
    public class TargetAssetService : ITargetAssetService
    {
        private readonly ITargetAssetRepository _repository;
        private readonly IDateTimeService _dateTimeService;

        public TargetAssetService(ITargetAssetRepository repository, IDateTimeService dateTimeService)
        {
            _repository = repository;
            _dateTimeService = dateTimeService;
        }

        /*
         using the dateTime service to get the current day 
         for isStartable stragint forward check for date and status
         for parentTargetAssetCount 
        using while loop to find the next parent till it reach null and that will be the root 
        but if there is cycle case then we use set to stop if we alreaady visited this node before 

         */

        public async Task<List<TargetAsset>> GetEnrichedTargetAssetsAsync()
        {
            var targetAssets = await _repository.GetTargetAssetsAsync();
            targetAssets = targetAssets.Where(x => x != null).ToList();

            for(int i = 0; i < targetAssets.Count; i++ )
            {
                // Add isStartable field
                var isStartable = _dateTimeService.Now.Day == 3 && targetAssets[i].status == "Running";
                targetAssets[i].isStartable = isStartable;

                // Add parentTargetAssetCount field
                var parentId = targetAssets[i].parentId;
                var parentTargetAssetCount = 0;
                var visited = new HashSet<int>();
                while (parentId != null && !visited.Contains(parentId.Value))
                {
                    visited.Add(parentId.Value);
                    parentId = targetAssets.FirstOrDefault(x => x.id == parentId)?.parentId;
                    parentTargetAssetCount++;
                }
                targetAssets[i].parentTargetAssetCount = parentTargetAssetCount;
            }

            return targetAssets;
        }
    }
}
