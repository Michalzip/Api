using System;
using Api.Controllers;
using Api.DTOS;
using Api.Mappings;
using Api.UnitTest.Fixtures;
using Api.Utils.Pagination;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.UnitTest.Test
{
    public class TagControllerTests
    {
        private readonly TagServiceFixture _tagServiceFixture;
        private readonly IMapper _mapper;

        public TagControllerTests()
        {
            _tagServiceFixture = new TagServiceFixture();
            _mapper = mockMapper.CreateMapper();
        }

        private readonly MapperConfiguration mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new TagsProfile());
        });

        [Fact]
        public async Task GetTags_Returns_PagedList_Of_TagsDto()
        {
            // Arrange
            var paginationParams = new PaginationParams { PageNumber = 1, PageSize = 10 };
            var desc = false;
            var searchString = "searchString";

            var controller = new TagsController(_tagServiceFixture.TagsServiceMock.Object, _mapper);

            // Act
            var result = await controller.GetTags(paginationParams, desc, searchString);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var tagsDto = Assert.IsAssignableFrom<PagedList<TagsDto>>(okResult.Value);

            Assert.NotNull(tagsDto);
            Assert.Equal(3, tagsDto.Count);
        }
    }
}
