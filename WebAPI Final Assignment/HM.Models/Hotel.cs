using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public decimal PinCode { get; set; }
        public decimal ContactNo { get; set; }
        public string ContactPerson { get; set; }
        public string Website { get; set; }
        public string facebook { get; set; }
        public string Twitter { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
