namespace Notes.Models
{
    public class Link
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Note Note { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}