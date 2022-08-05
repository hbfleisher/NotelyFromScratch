using NotelyFromScratch.Models;
using System.Collections.Generic;
using System;
using System.Linq;


namespace NotelyFromScratch.Respository
{
    public class NoteRepository : INoteRepository
    {
        private readonly List<NoteModel> _notes;

        public NoteRepository()
        {
            _notes = new List<NoteModel>();
        }
        public NoteModel FindNoteById(Guid id)
        {
            var note = _notes.Find(note => note.Id == id);
            //if (note == null)
            //{
            //    return  No note NoteModel.object
            //}
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
