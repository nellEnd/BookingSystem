using BookingSystem.Core.Dtos;
using BookingSystem.Core.Interfaces;
using BookingSystem.Core.Models;
using BookingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Infrastructure.Services
{
    public class BookingService:IBookingInterface
    {
        private readonly BookingSystemContext _context;
        private readonly IRoomInterface _roomService;

        public BookingService(BookingSystemContext context, IRoomInterface roomService)
        {
            _context = context;
            _roomService = roomService;
        }

        public async Task<List<BookingDto>?> GetUserBookings(int userId)
        {
            var bookings = await _context.Bookings.Where(b => b.UserId == userId).ToListAsync();

            if (bookings == null || bookings.Count == 0)
                return null;

            var dtos = new List<BookingDto>();

            foreach (var booking in bookings)
            {
                var room = await _roomService.GetRoomById(booking.RoomId);
                dtos.Add(new BookingDto
                {
                    RoomName = room.Name,
                    StartTime = booking.StartTime,
                    EndTime = booking.EndTime
                });
            }

            return dtos;
        }

        public async Task<bool> CreateBooking(BookingDto dto, int userId)
        {
            var room = await _roomService.GetRoomByName(dto.RoomName);

            if (room == null)
                return false;

            Booking b = new()
            {
                UserId = userId,
                RoomId = room.Id,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime
            };

             _context.Add(b);
            await _context.SaveChangesAsync();

            await _roomService.UpdateBookingString(b.Id, b.RoomId);
            return true;
        }
    }
}
