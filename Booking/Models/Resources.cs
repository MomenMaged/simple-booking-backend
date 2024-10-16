﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Booking.Models
{
    public class Resources
    {
        [Key]
        public int Id
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public int Quantity
        {
            get;
            set;
        }

        public IList<Booking> Bookings
        {
            get;
            set;
        }

    }
}
