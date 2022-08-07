using Microsoft.AspNetCore.Mvc;
using NotelyFromScratch.Models;
using System.Diagnostics;
using NotelyFromScratch.Repository;

namespace NotelyFromScratch.Controllers
{
    public class NoteController : Controller
    {
        static readonly INoteRepository _noteRepository = new NoteRepository();

        public IActionResult Index()
        {
            var notes = _noteRepository.GetAllNotes().Where(note => note.IsDeleted == false);

            return View(notes);
        }
        [HttpGet]
        public IActionResult FindNoteDetail(string search_text = "")
        {
            if (search_text != "" && search_text is not null)
            {
                IEnumerable<NoteModel> notes = _noteRepository.FindNotesByDetails(search_text);
                return View(notes);
            }
            return View();
        }

        [HttpGet]
        public IActionResult FindNoteSubject(string search_text = "")
        {
            if (search_text != "" && search_text is not null)
            {
                IEnumerable<NoteModel> notes = _noteRepository.FindNotesBySubject(search_text);
                return View(notes);
            }
            else
            {
                return View();
            }
        }
        public IActionResult SaveSuccessful()
        {
            return View("Success");
        }

        [HttpGet]
        public IActionResult NoteEditor(Guid id = default)
        {
            if (id != Guid.Empty)
            {
                var note = _noteRepository.FindNoteById(id);
            }
            return View();
        }

        [HttpPost]
        public IActionResult NoteEditor(NoteModel noteModel)
        {
            if (ModelState.IsValid)
            {
                var date = DateTime.Now;
                if (noteModel != null && noteModel.Id == Guid.Empty)
                {
                    noteModel.Id = Guid.NewGuid();
                    noteModel.CreatedDate = date;
                    noteModel.LastModifiedDate = date;
                    _noteRepository.SaveNote(noteModel);
                }
                else
                {
                    var note = _noteRepository.FindNoteById(noteModel.Id);
                    note.LastModifiedDate = date;
                    note.Subject = noteModel.Subject;
                    note.Detail = noteModel.Detail;
                }
                return RedirectToAction("SaveSuccessful");
            }
            else
            {
                return View();
            }
          
        }

        public IActionResult NoteDetail(Guid id)
        {
            var note = _noteRepository.FindNoteById(id);
            if (note != null)
            { 
                return View(note);
            }
            return View("Index");
        }

        public IActionResult DeleteNote(Guid id)
        {
            var note = _noteRepository.FindNoteById(id);
            note.IsDeleted = true;

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}