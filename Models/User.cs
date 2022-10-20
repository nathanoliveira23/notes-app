namespace Notes.Models
{
    public class User
    {
        private DateTime _date = DateTime.Now;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Avatar { get; set; }
        public string CreatedAt { get => this._date.ToString("dd/MM/yyyy HH:mm"); set {} }
        public string UpdatedAt { get => this._date.ToString("dd/MM/yyyy HH:mm"); set {} }
    }
}