using System;
using Api.EF.Entities;
using Api.Repositories;
using Moq;

namespace Api.UnitTest.Fixtures
{
    public class RepositoryServiceFixture
    {
        public Mock<ITagRepository> TagsRepositoryMock { get; private set; }
        public Mock<HttpClient> HttpClientMock { get; private set; }

        public RepositoryServiceFixture()
        {
            TagsRepositoryMock = new Mock<ITagRepository>();

            HttpClientMock = new Mock<HttpClient>();

            var tagDataList = new List<Tags>
            {
                new Tags { Name = "Tag1", Count = 10 },
                new Tags { Name = "Tag2", Count = 20 },
                new Tags { Name = "Tag3", Count = 30 }
            };

            TagsRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(tagDataList);
        }
    }
}
