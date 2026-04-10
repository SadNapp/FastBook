using FastBook.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastBook.Interfaces
{
    public interface ITagService
    {

        IEnumerable<string> ParseTags(string text);
        Task<List<Note>> GetNotesByTagsAsync(List<string> selectedTags);
        Task UpdateNoteTagsAsync(Guid noteId, IEnumerable<string> tagNames);
    }
}
