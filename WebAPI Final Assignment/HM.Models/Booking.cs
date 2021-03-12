using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public System.DateTime BookingDate { get; set; }
        public int roomID { get; set; }
        public string bookingStatus { get; set; }

    }
}
