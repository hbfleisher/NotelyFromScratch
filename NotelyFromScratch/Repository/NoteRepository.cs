using NotelyFromScratch.Models;
using System.Collections.Generic;
using System;
using System.Linq;


namespace NotelyFromScratch.Repository
{
    public class NoteRepository : INoteRepository
    {
        private readonly List<NoteModel> _notes = new List<NoteModel>();

        public NoteRepository()
        {
            SaveNote(new NoteModel { Subject="Second Try",Detail="Never Too Late", CreatedDate=DateTime.Now, Id=Guid.NewGuid(), IsDeleted=false, LastModifiedDate=DateTime.Now});
        }
        public NoteModel FindNoteById(Guid id)
        {
            var note = _notes.Find(note => note.Id == id);
            return note;
        }
        public List<NoteModel> FindNotesByDetails(String search_text)
        {
            var notes = new List<NoteModel>(_notes.FindAll(note => note.Detail == search_text));
            return notes;
        }
        public IEnumerable<NoteModel> GetAllNotes()
        {
            return _notes;
        }
        public void SaveNote(NoteModel note)
        {
            _notes.Add(note);
        }
        public void DeleteNote(NoteModel note)
        {
            _notes.Remove(note);
        }
    }
}
