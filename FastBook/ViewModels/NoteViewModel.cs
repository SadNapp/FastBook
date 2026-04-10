using System.Collections.ObjectModel;
using System.Linq; // Додай цей using для .Any() та .FirstOrDefault()
using System.Threading.Tasks;
using FastBook.Interfaces;
using FastBook.Models;

namespace FastBook.ViewModels
{
    public class NoteViewModel : BaseViewModel
    {
        private readonly INoteService _noteService;
        private readonly ITagService _tagService;
        

        private Note? _currentNote;

        public NoteViewModel(INoteService noteService, ITagService tagService)
        {
            _noteService = noteService;
            _tagService = tagService;
            
        }

        // ЗАЛИШАЄМО ТІЛЬКИ ОДИН БЛОК CurrentNote
        public Note? CurrentNote
        {
            get => _currentNote;
            set
            {
                _currentNote = value;
                OnPropertyChanged();
            }
        }

        public async Task OnSaveNote()
        {
            if (CurrentNote == null) return;

            // 1. Отримуємо теги з тексту
            var tags = _tagService.ParseTags(CurrentNote.Content ?? string.Empty);

            if (tags.Any())
            {
                
            }

            try
            {
                // 2. Оновлюємо зв'язки з тегами в базі (не забудь цей рядок!)
                await _tagService.UpdateNoteTagsAsync(CurrentNote.Id, tags);

                // 3. Зберігаємо саму нотатку
                await _noteService.UpdateNoteAsync(CurrentNote);
               
            }
            catch
            {
                
            }
        }

        public async Task LoadNoteAsync(string category)
        {
            var notes = await _noteService.GetAllNotesAsync();
            // Додаємо null-check для Category, щоб не було warnings
            var note = notes.FirstOrDefault(n => n.Category == category);

            if (note == null)
            {
                note = new Note { Category = category, Content = "", Priority = 1 };
                await _noteService.AddNoteAsync(note);
            }

            CurrentNote = note;
        }

        public async Task SaveCurrentNoteAsync()
        {
            if (CurrentNote != null)
            {
                await _noteService.UpdateNoteAsync(CurrentNote);
            }
        }
    }
}