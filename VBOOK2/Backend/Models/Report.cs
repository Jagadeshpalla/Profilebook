namespace VBOOK2.Backend.Models
{
    public class Report
    {
        public int ReportId { get; set; }
        public int ReportedUserId { get; set; }
        public int ReportingUserId { get; set; }
        public string Reason { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
