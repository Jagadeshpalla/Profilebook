namespace VBOOK2.Backend.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public string PostImage { get; set; }
        public string Status { get; set; }
        public User User { get; set; } // Navigation property
    }
}
