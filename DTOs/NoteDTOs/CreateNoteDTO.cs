using Notes.Models;

namespace Notes.DTOs.NoteDTOs
{
    public class CreateNoteDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }   
        public IList<Tag> Tags { get; set; }
        public IList<Link> Links { get; set; }
    }
}