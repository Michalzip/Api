using Api.DTOS;
using Api.EF.Entities;
using Api.Services.StackExchange.TagsService;
using Api.Utils.Pagination;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class TagsController : Controller
    {
        private readonly ITagsService _tagService;
        private readonly IMapper _mapper;

        public TagsController(ITagsService tagService, IMapper mapper)
        {
            _tagService = tagService;
            _mapper = mapper;
        }

        [HttpGet("Tags")]
        public async Task<ActionResult<PagedList<TagsDto>>> GetTags(
            [FromQuery] PaginationParams paginationParams,
            bool desc,
            string searchString
        )
        {
            var data = await _tagService.GetAllTags(desc, searchString);

            var tagsDto = _mapper.Map<List<Tags>, List<TagsDto>>(data);

            return Ok(
                PagedList<TagsDto>.ToPagedList(
                    tagsDto,
                    paginationParams.PageNumber,
                    paginationParams.PageSize
                )
            );
        }

        [HttpGet("RefreshTags")]
        public async Task<ActionResult> RefreshTags()
        {
            await _tagService.RefreshTags();

            return Ok("Tags Are Force Refreshed");
        }
    }
}
