using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Models
{
    public class Booking
    {
        [Key]
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
        public int ResourcesId
        {
            get;
            set;
        }

        public Resources Resource;
    }
}
