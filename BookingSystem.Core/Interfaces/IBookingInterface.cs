using BookingSystem.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Core.Interfaces
{
    public interface IBookingInterface
    {
        Task<List<BookingDto>?> GetUserBookings(int userId);
        Task<bool> CreateBooking(BookingDto dto, int userId);
    }
}
