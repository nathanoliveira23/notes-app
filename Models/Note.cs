namespace Notes.Models
{
    public class Note
    {
        public Note()
        {
            Tags = new List<Tag>();
            Links = new List<Link>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<Link> Links { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}