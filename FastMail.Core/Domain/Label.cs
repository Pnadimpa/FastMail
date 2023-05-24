using System.ComponentModel.DataAnnotations;

namespace FastMail.Core.Domain
{
    public class Label : BaseEntity
    {
        public DateTime DateCreatedUtc { get; set; }

        [Required(ErrorMessage = "Mail type is required")]
        public string MailType { get; set; }

        public string? RecipientId { get; set; }

        [Required(ErrorMessage = "Recipient name is required")]
        public string RecipientName { get; set; }

        public string? RecipientEmail { get; set; }

        public string? Remarks { get; set; }

        public string? SenderId { get; set; }

        [Required(ErrorMessage = "Sender name is required")]
        public string SenderName { get; set; }

       public string? SenderEmail { get; set; }
    }
}





