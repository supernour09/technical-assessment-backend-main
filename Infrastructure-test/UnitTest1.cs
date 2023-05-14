using Domain;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Infrastructure.TargetAssetRepo.Tests
{
    public class UnitTest1
    {
        private readonly TargetAssetRepository _targetAssetRepository;
        private readonly Mock<IHttpClientFactory> _mockHttpClientFactory;

        public UnitTest1()
        {
            _mockHttpClientFactory = new Mock<IHttpClientFactory>();
            _targetAssetRepository = new TargetAssetRepository(_mockHttpClientFactory.Object);
        }

        [Fact]
        public async Task GetTargetAssetsAsync_ReturnsListOfTargetAssets()
        {
            // Arrange
            var expectedTargetAssets = new List<TargetAsset>
            {
                new TargetAsset { id = 1, name = "TargetAsset 1", status = "Running", parentId = null },
                new TargetAsset { id = 2, name = "TargetAsset 2", status = "Stopped", parentId = 1 }
            };

            var httpMessageHandler = new Mock<HttpMessageHandler>();
            httpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonSerializer.Serialize(expectedTargetAssets), Encoding.UTF8, "application/json")
                });

            var httpClient = new HttpClient(httpMessageHandler.Object);
            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            // Act
            var result = await _targetAssetRepository.GetTargetAssetsAsync();

            // Assert
            Assert.Equal(expectedTargetAssets.Count, result.Count);
            Assert.Equal(expectedTargetAssets[0].id, result[0].id);
            Assert.Equal(expectedTargetAssets[0].name, result[0].name);
            Assert.Equal(expectedTargetAssets[0].status, result[0].status);
            Assert.Equal(expectedTargetAssets[0].parentId, result[0].parentId);
            Assert.Equal(expectedTargetAssets[1].id, result[1].id);
            Assert.Equal(expectedTargetAssets[1].name, result[1].name);
            Assert.Equal(expectedTargetAssets[1].status, result[1].status);
            Assert.Equal(expectedTargetAssets[1].parentId, result[1].parentId);
        }
    }
}
