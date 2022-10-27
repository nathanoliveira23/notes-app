using Notes.Models;

namespace Notes.DTOs.NoteDTOs
{
    public class CreateNoteDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }   
        public int UserId { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<Link> Links { get; set; }
    }
}