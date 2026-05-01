namespace taskCompanySystem.Models
{
    public class ContactFeedback
    {
        public int Id { get; set; }
        public string SenderEmail { get; set; }
        public string Message { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.Now;
    }
}
