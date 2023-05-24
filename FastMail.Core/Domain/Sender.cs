
namespace FastMail.Core.Domain
{
    public class Sender : BaseEntity
    {
        public string Name { get; set; }

       public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public string Remark { get; set; }

        public DateTime DateCreatedUtc { get; set; }
    }
}
