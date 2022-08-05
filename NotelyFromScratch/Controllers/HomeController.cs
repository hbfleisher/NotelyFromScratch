using Microsoft.AspNetCore.Mvc;
using NotelyFromScratch.Models;
using System.Diagnostics;
using NotelyFromScratch.Respository;

namespace NotelyFromScratch.Controllers
{
    public class HomeController : Controller
    {
        private readonly INoteRepository _noteRepository;
        public HomeController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var notes = _noteRepository.GetAllNotes().Where(note => note.IsDeleted == false);

            return View(notes);
        }

        public IActionResult NoteDetail(Guid id)
        {
            if (id != Guid.Empty)
            {
                var note = _noteRepository.FindNoteById(id);
                return View(note);
            }
            return View();
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
            var date = DateTime.Now;
            if (noteModel != null && noteModel.Id != Guid.Empty)
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
            return RedirectToAction("Index");
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