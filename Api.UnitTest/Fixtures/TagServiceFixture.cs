using System;
using Api.EF.Entities;
using Api.Services.StackExchange.TagsService;
using Moq;

namespace Api.UnitTest.Fixtures
{
    public class TagServiceFixture
    {
        public Mock<ITagsService> TagsServiceMock { get; private set; }

        public TagServiceFixture()
        {
            TagsServiceMock = new Mock<ITagsService>();

            var tags = new List<Tags>
            {
                new Tags { Id = 1, Name = "Tag1" },
                new Tags { Id = 2, Name = "Tag2" },
                new Tags { Id = 3, Name = "Tag3" }
            };

            TagsServiceMock
                .Setup(x => x.GetAllTags(It.IsAny<bool>(), It.IsAny<string>()))
                .ReturnsAsync(tags);
        }
    }
}
