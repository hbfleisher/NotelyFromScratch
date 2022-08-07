
namespace NotelyFromScratch.Models
{
    public class AppUser
    {
        public string UserName { get; set; }
        public IEnumerable<NoteModel> Notes {get; set;}
    }
}
