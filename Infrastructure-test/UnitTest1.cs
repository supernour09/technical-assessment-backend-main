using Domain;
using Moq;
using System.Text;
using System.Text.Json;

namespace Infrastructure.TargetAssetRepo.Tests
{
    public class TargetAssetRepositoryTests
    {
        [Fact]
        public async Task GetTargetAssetsAsync_ShouldReturnListOfTargetAssets_WhenCalled()
        {
            // Arrange
            var expectedTargetAssets = new List<TargetAsset>
            {
                new TargetAsset { id = 1, name = "TargetAsset 1" },
                new TargetAsset { id = 2, name = "TargetAsset 2" }
            };

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            var mockHttpClient = new Mock<HttpClient>();
            mockHttpClientFactory.Setup(x => x.CreateClient("TargetAssetAPI")).Returns(mockHttpClient.Object);

            var json = JsonSerializer.Serialize(expectedTargetAssets);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = content };
            mockHttpClient
                .Setup(x => x.GetAsync("/targetAsset", CancellationToken.None))
                .ReturnsAsync(response);

            var repository = new TargetAssetRepository(mockHttpClientFactory.Object);

            // Act
            var actualTargetAssets = await repository.GetTargetAssetsAsync();

            // Assert
            Assert.Equal(expectedTargetAssets.Count, actualTargetAssets.Count);
            Assert.Equal(expectedTargetAssets[0].id, actualTargetAssets[0].id);
            Assert.Equal(expectedTargetAssets[0].name, actualTargetAssets[0].name);
            Assert.Equal(expectedTargetAssets[1].id, actualTargetAssets[1].id);
            Assert.Equal(expectedTargetAssets[1].name, actualTargetAssets[1].name);
        }
    }
}
