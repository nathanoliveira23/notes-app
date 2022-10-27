using System.ComponentModel.DataAnnotations.Schema;

namespace Notes.Models
{
    public class Link
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int? NoteId { get; set; }

        [ForeignKey("NoteId")]
        public virtual Note? Note { get; private set; }
    }
}