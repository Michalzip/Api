using System;
using Api.EF;
using Api.EF.Entities;
using Api.IntegrationTest.Factory;
using Api.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Api.IntegrationTest.Test
{
    public class TagRepositoryTest : IClassFixture<ApiFactory<Program>>
    {
        private readonly ApiFactory<Program> _apiFactory;

        public TagRepositoryTest(ApiFactory<Program> apiFactory)
        {
            _apiFactory = apiFactory;
        }

        [Fact]
        public async Task AddAllAsync_ShouldAddTagsToDatabase()
        {
            // Arrange
            var scope = _apiFactory.Services.GetService<IServiceScopeFactory>()!.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<DatabaseContext>();

            var repository = new TagRepository(dbContext);
            var tags = new List<Tags>
            {
                new Tags { Id = 1, Name = "tag1" },
                new Tags { Id = 2, Name = "tag2" },
                new Tags { Id = 3, Name = "tag3" }
            };

            // Act
            await repository.AddAllAsync(tags);

            // Assert
            var addedTags = await repository.GetAllAsync();

            Assert.NotNull(dbContext);
            Assert.Equal(3, addedTags.Count);
        }
    }
}
