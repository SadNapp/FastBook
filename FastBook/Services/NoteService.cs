using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FastBook.Data;
using FastBook.Models;
using FastBook.Interfaces;

namespace FastBook.Services
{
    public class NoteService : INoteService
    {
        private readonly NotesDbContext _context;

        public NoteService()
        {
            _context = new NotesDbContext();
        }

        public async Task<List<Note>> GetAllNotesAsync() 
        {
            return await _context.Notes.ToListAsync();
        }

        public async Task AddNoteAsync(Note note) 
        {
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateNoteAsync(Note note) 
        {
            _context.Entry(note).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNoteAsync(Guid id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note != null)
            {
                _context.Notes.Remove(note);
                await _context.SaveChangesAsync();
            }
        }
        
    }
}