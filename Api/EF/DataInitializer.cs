using Api.Services.StackExchange.TagsService;

namespace Api.EF
{
    public class DataInitializer : IInitializer
    {
        private readonly ITagsService _tagService;

        public DataInitializer(ITagsService tagService)
        {
            _tagService = tagService;
        }

        public async Task InitAsync()
        {
            await _tagService.InitializeTagsFromSo();

            var Tags = await _tagService.GetAllTags();

            _tagService.CalculateTagPercentages(Tags);
        }
    }
}
