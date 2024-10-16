using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.ViewModels
{
    public class ResponseBooking
    {
        public int Id
        {
            get;
            set;
        }

        public DateTime DateFrom
        {
            get;
            set;
        }
        public DateTime DateTo
        {
            get;
            set;
        }
        public int BookedQuantity
        {
            get;
            set;
        }
    }
}
