using Api.Services.StackExchange.TagsService;
using Api.UnitTest.Fixtures;

namespace Api.UnitTest.Test
{
    public class TagServiceTests
    {
        private readonly RepositoryServiceFixture _tagRepositoryFixture;

        public TagServiceTests()
        {
            _tagRepositoryFixture = new RepositoryServiceFixture();
        }

        [Fact]
        public async void CalculateTagPercentages_ShouldCalculateCorrectPercentages()
        {
            // Arrange
            var tagService = new TagsService(
                _tagRepositoryFixture.TagsRepositoryMock.Object,
                _tagRepositoryFixture.HttpClientMock.Object
            );

            // Act
            var tagDataList = await tagService.GetAllTags();
            var result = tagService.CalculateTagPercentages(tagDataList);

            // Assert
            Assert.Equal(17, result[0].Count);
            Assert.Equal(33, result[1].Count);
            Assert.Equal(50, result[2].Count);
        }
    }
}
