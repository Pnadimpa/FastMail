using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastMail.Core.Domain
{
    public class TrackingPoint
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DateUtc { get; set; }

       public decimal Latitude { get; set; }

       public decimal Longitude { get; set; }

       public string IconClass { get; set; }
    }
}

