using System;
using Api.DTOS;
using Api.EF.Entities;

namespace Api.Services.StackExchange.TagsService
{
    public interface ITagsService
    {
        public Task InitializeTagsFromSo();
        public List<TagsDto> CalculateTagPercentages(List<Tags> list);
        public Task<List<Tags>> GetAllTags(bool desc = false, string searchString = null);
        public Task<bool> RefreshTags();
    }
}
