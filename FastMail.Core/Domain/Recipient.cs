
using System.ComponentModel.DataAnnotations;

namespace FastMail.Core.Domain
{
    public class Recipient : BaseEntity
    {
        [Required(ErrorMessage = "RecipientId is required")]
        public string RecipientId { get; set; }
         public string? RecipientDepartment { get; set; }

        [Required(ErrorMessage = "RecipientName is required")]
        public string RecipientName { get; set; }

        public string? RecipientEmail { get; set; }

        public string? RecipientAddress { get; set; }

        public string? RecipientAddressAbr { get; set; }

        public string? RecipientCity { get; set; }

        public string? RecipientState { get; set; }

        public string? RecipientZip { get; set; }

        public string? RecipientDepartmentAbr { get; set; }

        public string? RecipientProgramName { get; set; }

        public string? RecipientDivision { get; set; }

        public string? RecipientRoomNumber { get; set; }

        public string? RecipientRouteNumber { get; set; }

        public string? RecipientMailStopNumber { get; set; }

        public string? RecipientDaysOfService { get; set; }

        public string? RecipientDaysAbr { get; set; }

        public string? RecipientTimeRestrictions { get; set; }

        public string? RecipientSameDayService { get; set; }

        public string? RecipientBillingCodeNumber { get; set; }

        public string? RecipientContactName { get; set; }

        public string? RecipientPhoneNumber { get; set; }

        public string? RecipientSite { get; set; }
    }
}