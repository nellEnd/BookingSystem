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
    public class RoomService:IRoomInterface
    {
        private readonly BookingSystemContext _context;

        public RoomService(BookingSystemContext context)
        {
            _context = context;
        }

        public async Task<Room> GetRoomById(int id)
        {
            return await _context.Rooms.Where(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Room> GetRoomByName(string name)
        {
            return await _context.Rooms.Where(r => r.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        }

        public async Task UpdateBookingString(int bookingId, int roomId)
        {
            var room = await GetRoomById(roomId);

            if (String.IsNullOrEmpty(room.BookingString))
                room.BookingString = bookingId.ToString();
            else
                room.BookingString = room.BookingString + "," + bookingId.ToString();

            await _context.SaveChangesAsync();
        }
    }
}
