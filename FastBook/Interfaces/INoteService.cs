using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FastBook.Models;

namespace FastBook.Interfaces
{
    public interface INoteService
    {
        Task<List<Note>> GetAllNotesAsync();

        Task AddNoteAsync(Note note);

        Task UpdateNoteAsync(Note note);

        Task DeleteNoteAsync(Guid id);
    }
}
