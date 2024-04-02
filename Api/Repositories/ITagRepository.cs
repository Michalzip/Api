using System;
using Api.EF.Entities;

namespace Api.Repositories
{
    public interface ITagRepository
    {
        public Task AddAllAsync(List<Tags> tags);
        public Task<List<Tags>> GetAllAsync();
        public Task<bool> FindAnyRecord();
        public Task ResetAndAddAllAsync(List<Tags> tags);
    }
}
