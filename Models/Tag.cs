namespace Notes.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Note Note { get; set; }
        public User User { get; set; }
    }
}