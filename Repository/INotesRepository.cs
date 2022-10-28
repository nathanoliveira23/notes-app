using Notes.Models;

namespace Notes.Repository
{
    public interface INotesRepository
    {
        public Task SaveNoteAsync(Note note);
        public Note GetNoteId(int id);
        public Task RemoveNoteAsync(Note note);
    }
}