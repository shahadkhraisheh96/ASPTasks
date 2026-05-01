using System.ComponentModel.DataAnnotations;

namespace companySystem.Models
{
    public class ContactMessage
    {

        [Key]
        public int MessageId { get; set; }

        [Required, MaxLength(100)]
        public string SenderName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string MessageBody { get; set; }

        public DateTime ReceivedAt { get; set; } = DateTime.Now;

        public bool IsRead { get; set; } = false;

    }
}
