using Api.DTOS;
using Api.EF.Entities;
using Api.Exceptions;
using Api.Helpers;
using Api.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Api.Services.StackExchange.TagsService
{
    public class TagsService : ITagsService
    {
        private readonly HttpClient _httpClient;
        private readonly ITagRepository _tagRepository;

        public TagsService(ITagRepository tagRepository, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _tagRepository = tagRepository;
        }

        public List<TagsDto> CalculateTagPercentages(List<Tags> list)
        {
            List<TagsDto> tagsWithPercentage = new List<TagsDto>();
            Dictionary<string, double> tagPercentages = new Dictionary<string, double>();

            long totalCount = 0;

            foreach (var tagData in list)
            {
                totalCount += tagData.Count;
            }

            foreach (var tagData in list)
            {
                double percentage = (double)tagData.Count / totalCount * 100;

                tagPercentages[tagData.Name] = percentage;
            }

            foreach (var tagPercentage in tagPercentages)
            {
                int roundedPercentage = (int)Math.Round(tagPercentage.Value);

                tagsWithPercentage.Add(
                    new TagsDto { Name = tagPercentage.Key, Count = roundedPercentage }
                );

                Console.WriteLine($"{tagPercentage.Key}: {tagPercentage.Value}%");
            }

            return tagsWithPercentage;
        }

        private async Task<List<Tags>> FetchTagsFromSo()
        {
            var listOfTags = new List<Tags>();

            for (int i = 0; i < 12; i++)
            {
                HttpResponseMessage response = await _httpClient.GetAsync(
                    $"https://api.stackexchange.com/2.3/tags?page={i + 1}&pagesize=100&order=asc&sort=name&site=stackoverflow&filter=!21k7qaosV)V8y5XQ1QjJd"
                );

                var decompressedData = await DecodeHelper.DecompressGZipStreamAsync(response);
                JObject jsonObject = JObject.Parse(decompressedData);

                if (!response.IsSuccessStatusCode)
                    throw new FetchDataException(
                        $"Can't fetch data from stack exchange api. details: {jsonObject["error_message"]}"
                    );

                var itemsArray = jsonObject["items"];

                var data = JsonConvert.DeserializeObject<List<Tags>>(itemsArray.ToString());

                foreach (var item in data)
                {
                    listOfTags.Add(item);
                }
            }

            return listOfTags;
        }

        public async Task InitializeTagsFromSo()
        {
            if (!await _tagRepository.FindAnyRecord())
            {
                var fetchedTags = await FetchTagsFromSo();

                await _tagRepository.AddAllAsync(fetchedTags);
            }
        }

        public async Task<List<Tags>> GetAllTags(bool desc = false, string searchString = "")
        {
            var listOfTags =
                await _tagRepository.GetAllAsync()
                ?? throw new NotFoundException("List of tags are missed");

            IEnumerable<Tags> sortedList = listOfTags;

            if (!String.IsNullOrEmpty(searchString))
            {
                sortedList = sortedList.Where(tag => tag.Name.Contains(searchString));
            }

            if (desc)
            {
                sortedList = sortedList.OrderByDescending(tag => tag.Count);
            }
            else
            {
                sortedList = sortedList.OrderBy(tag => tag.Count);
            }

            return sortedList.ToList();
        }

        public async Task<bool> RefreshTags()
        {
            var listOfTags = await FetchTagsFromSo();

            await _tagRepository.ResetAndAddAllAsync(listOfTags);

            return true;
        }
    }
}
