using Notes.Models;

namespace Notes.Repository
{
    public interface INotesRepository
    {
        public Task SaveNote(Note note);
        public Note GetNoteId(int id);
        public Task RemoveNote(Note note);
    }
}