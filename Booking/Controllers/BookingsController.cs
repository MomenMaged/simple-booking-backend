using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Booking.Models;
using Booking.ViewModels;

namespace Booking.Controllers
{
    [Route("api/resources/{resourceId}/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly ResContext _context;

        public BookingsController(ResContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseBooking>>> GetBookings(int resourceId)
        {
            List<ResponseBooking> result = new List<ResponseBooking>();
            List<Models.Booking> response = await _context.Bookings.Where(b => b.ResourcesId == resourceId).ToListAsync();

            foreach(Models.Booking book in response)
            {
                result.Add(new ResponseBooking
                {
                    Id = book.Id,
                    DateFrom = book.DateFrom,
                    DateTo = book.DateTo,
                    BookedQuantity = book.BookedQuantity,
                });
            }

            return result;

        }

        [HttpGet("{bookId}")]
        public async Task<ActionResult<ResponseBooking>> GetBooking(int resourceId, int bookId)
        {
            var book = await _context.Bookings.Where(b => b.Id == bookId && b.ResourcesId == resourceId).FirstOrDefaultAsync();

            if (book == null)
            {
                return NotFound();
            }

            return new ResponseBooking
            {
                Id = book.Id,
                DateFrom = book.DateFrom,
                DateTo = book.DateTo,
                BookedQuantity = book.BookedQuantity,
            };
        }

        [HttpPost]
        public async Task<ActionResult<ResponseBooking>> PostBooking(int resourceId, RequestBooking booking) 
        {

            if (booking.DateFrom > booking.DateTo)
                return BadRequest();

            Resources resource = await GetResource(resourceId); 

            if (resource == null)
                return NotFound();

            int bookedQuantityNum = GetResourceBookingQuantity(resourceId); 

            if ((bookedQuantityNum + booking.BookedQuantity) > resource.Quantity)
                return BadRequest();
            
            Models.Booking payload = new Models.Booking
            {
                DateFrom = booking.DateFrom,
                DateTo = booking.DateTo,
                BookedQuantity = booking.BookedQuantity,
                ResourcesId = resourceId
            };
            _context.Bookings.Add(payload);
            await _context.SaveChangesAsync();

            return new ResponseBooking { 
                Id = payload.Id,
                DateFrom = payload.DateFrom,
                DateTo = payload.DateTo,
                BookedQuantity = payload.BookedQuantity,
            };
        }

        private async Task<Resources> GetResource(int resourceId)
        {
            return await _context.Resources.FirstOrDefaultAsync(e => e.Id == resourceId);
        }

        private int GetResourceBookingQuantity(int resourceId)
        {
            return _context.Bookings.Where(e => e.ResourcesId == resourceId).Sum(b => b.BookedQuantity);
        }
    }
}
