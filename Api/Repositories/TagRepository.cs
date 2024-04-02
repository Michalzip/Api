using System;
using Api.EF;
using Api.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<Tags> _tags;

        public TagRepository(DatabaseContext context)
        {
            _context = context;
            _tags = _context.Tags;
        }

        public async Task AddAllAsync(List<Tags> tags)
        {
            await _context.Tags.AddRangeAsync(tags);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Tags>> GetAllAsync() => await _tags.ToListAsync();

        public async Task<bool> FindAnyRecord()
        {
            bool result = await _context.Tags.AnyAsync();

            return result;
        }

        public async Task ResetAndAddAllAsync(List<Tags> tags)
        {
            _context.Tags.RemoveRange(await _context.Tags.ToListAsync());

            await _context.Tags.AddRangeAsync(tags);

            await _context.SaveChangesAsync();
        }
    }
}
