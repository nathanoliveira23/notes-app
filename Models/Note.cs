namespace Notes.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
        public IList<Tag> Tags { get; set; }
        public IList<Link> Links { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}