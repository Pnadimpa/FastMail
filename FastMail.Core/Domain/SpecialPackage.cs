using System.ComponentModel.DataAnnotations;

namespace FastMail.Core.Domain
{
    public class SpecialPackage : BaseEntity
    {
        public DateTime DateCreatedUtc { get; set; }

        [Required(ErrorMessage = "Mail type is required")]
        public string MailType { get; set; }

        public string? RecipientId { get; set; }

        [Required(ErrorMessage = "Recipient name is required")]
        public string RecipientName { get; set; }

       public string? RecipientEmail { get; set; }

        [Required(ErrorMessage = "Recipient address is required")]
        public string RecipientAddress { get; set; }

       public string? RecipientCity { get; set; }

       public string? RecipientState { get; set; }

        public string? RecipientZip { get; set; }


       public string? Remarks { get; set; }

        public string? SenderId { get; set; }

        [Required(ErrorMessage = "Sender name is required")]
        public string SenderName { get; set; }

        public string? SenderEmail { get; set; }

        [Required(ErrorMessage = "Sender address is required")]
        public string SenderAddress { get; set; }

        public string? SenderCity { get; set; }

       public string? SenderState { get; set; }

       public string? SenderZip { get; set; }
    }
}





