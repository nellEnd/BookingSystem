using BookingSystem.Core.Models;

namespace BookingSystem.Core.Interfaces
{
    public interface IRoomInterface
    {
        Task<Room> GetRoomById(int id);
        Task<Room> GetRoomByName(string name);
        Task UpdateBookingString(int bookingId, int roomId);
    }
}
