using Application.Services;
using Domain;
using Infrastructure.IDateTimeService;
using Infrastructure.TargetAssetRepo;
using Moq;

namespace Application_test
{
    public class UnitTest1
    {
        private readonly Mock<ITargetAssetRepository> _targetAssetRepositoryMock;
        private readonly Mock<IDateTimeService> _dateTimeServiceMock;
        private readonly TargetAssetService _targetAssetService;

        public UnitTest1()
        {
            _targetAssetRepositoryMock = new Mock<ITargetAssetRepository>();
            _dateTimeServiceMock = new Mock<IDateTimeService>();
            _targetAssetService = new TargetAssetService(_targetAssetRepositoryMock.Object, _dateTimeServiceMock.Object);
        }

        [Fact]
        public async Task GetEnrichedTargetAssetsAsync_WhenCalled_ReturnsEnrichedTargetAssets()
        {
            // Arrange
            var targetAsset1 = new TargetAsset { id = 1, status = "Running", parentId = 2 };
            var targetAsset2 = new TargetAsset { id = 2, status = "Stopped", parentId = null };
            var targetAsset3 = new TargetAsset { id = 3, status = "Running", parentId = 2 };
            var targetAssets = new List<TargetAsset> { targetAsset1, targetAsset2, targetAsset3 };
            _targetAssetRepositoryMock.Setup(x => x.GetTargetAssetsAsync()).ReturnsAsync(targetAssets);

            _dateTimeServiceMock.Setup(x => x.Now).Returns(new DateTime(2023, 5, 3));

            // Act
            var result = await _targetAssetService.GetEnrichedTargetAssetsAsync();

            // Assert
            Assert.Equal(3, result.Count);

            Assert.Equal(true, result[0].isStartable);
            Assert.Equal(1, result[0].parentTargetAssetCount);

            Assert.Equal(false, result[1].isStartable);
            Assert.Equal(0, result[1].parentTargetAssetCount);

            Assert.Equal(true, result[2].isStartable);
            Assert.Equal(1, result[2].parentTargetAssetCount);
        }
    }
}