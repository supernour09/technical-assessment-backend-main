using Application.Services;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace Demo_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TargetAssetController : ControllerBase
    {
        private readonly ITargetAssetService _service;

        public TargetAssetController(ITargetAssetService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<TargetAsset>>> Get()
        {
            var targetAssets = await _service.GetEnrichedTargetAssetsAsync();
            return targetAssets;
        }
    }
}