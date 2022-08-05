namespace NotelyFromScratch.Models
{
    public class NoteModel
    {
        public Guid Id {get; set;}
        public String Subject {get; set;}
        public String Detail {get; set;}
        public DateTime CreatedDate {get; set;}
        public DateTime LastModifiedDate {get; set;}
        public Boolean IsDeleted {get; set;}
    }
}
