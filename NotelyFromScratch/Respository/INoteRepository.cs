using NotelyFromScratch.Models;

namespace NotelyFromScratch.Respository
{
    public interface INoteRepository
    {
        void DeleteNote(NoteModel note);
        NoteModel FindNoteById(Guid id);
        List<NoteModel> FindNotesByDetails(string search_text);
        void SaveNote(NoteModel note);
        IEnumerable<NoteModel> GetAllNotes();
    }
}