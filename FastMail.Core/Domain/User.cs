using System.ComponentModel.DataAnnotations;

namespace FastMail.Core.Domain
{
    public class User : BaseEntity
    {        
        [Required(ErrorMessage = "UserId is required")]
        public string? UserId { get; set; }

        public string? UserName { get; set; }

        public string? UserEmail { get; set; }

        //[Required(ErrorMessage = "Recipient address is required")]
        public string? UserPhone { get; set; }


    }
}
