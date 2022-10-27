using System.ComponentModel.DataAnnotations.Schema;

namespace Notes.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? NoteId { get; set; }

        [ForeignKey("NoteId")]
        public virtual Note? Note { get; private set; }
    }
}