namespace VBOOK2.Backend.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string MessageContent { get; set; }
        public DateTime TimeStamp { get; set; }
        public User Sender { get; set; }
        public User Receiver { get; set; }
    }
}
