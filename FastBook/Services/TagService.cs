using FastBook.Data;
using FastBook.Models;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using FastBook.Interfaces;

namespace FastBook.Services
{
    public class TagService : ITagService
    {

        private readonly NotesDbContext _context;

        public TagService(NotesDbContext context)
        {
            _context = context;
        }

        public IEnumerable<string> ParseTags(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return Enumerable.Empty<string>();

            
            return Regex.Matches(text, @"#\w+")
                        .Select(m => m.Value.ToLower())
                        .Distinct();
        }

        public async Task<List<Note>> GetNotesByTagsAsync(List<string> selectedTags)
        {
           
            return await _context.Notes
                .Where(n => n.Tags.Any(t => selectedTags.Contains(t.Name)))
                .ToListAsync();
        }

        public async Task UpdateNoteTagsAsync(Guid noteId, IEnumerable<string> tagNames)
        {
            
            var note = await _context.Notes
                .Include(n => n.Tags)
                .FirstOrDefaultAsync(n => n.Id == noteId);

            if (note == null) return;

            
            note.Tags.Clear();

            foreach (var name in tagNames)
            {
               
                var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Name == name);

               
                if (tag == null)
                {
                    tag = new Tag { Name = name };
                    _context.Tags.Add(tag);
                }

               
                note.Tags.Add(tag);
            }

            await _context.SaveChangesAsync();
        }
    }
}
