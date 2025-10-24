using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DAO
{
    public class RoomInformationDAO
    {
        private static RoomInformationDAO? _instance;
        private static readonly object _lock = new object();

        public static RoomInformationDAO Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new RoomInformationDAO();
                        }
                    }
                }
                return _instance;
            }
        }

        public List<RoomInformation> GetRooms()
        {
            using var context = new FUMiniHotelManagementContext();
            return context.RoomInformations
                .Include(r => r.RoomType)
                .ToList();
        }

        public RoomInformation? GetRoomById(int id)
        {
            using var context = new FUMiniHotelManagementContext();
            return context.RoomInformations
                .Include(r => r.RoomType)
                .FirstOrDefault(r => r.RoomID == id);
        }

        public void AddRoom(RoomInformation room)
        {
            using var context = new FUMiniHotelManagementContext();
            context.RoomInformations.Add(room);
            context.SaveChanges();
        }

        public void UpdateRoom(RoomInformation room)
        {
            using var context = new FUMiniHotelManagementContext();
            context.RoomInformations.Update(room);
            context.SaveChanges();
        }

        public void DeleteRoom(int id)
        {
            using var context = new FUMiniHotelManagementContext();
            var room = context.RoomInformations.FirstOrDefault(r => r.RoomID == id);
            if (room != null)
            {
                room.RoomStatus = 2; // Soft delete
                context.SaveChanges();
            }
        }

        public List<RoomInformation> SearchRooms(string keyword)
        {
            using var context = new FUMiniHotelManagementContext();
            return context.RoomInformations
                .Include(r => r.RoomType)
                .Where(r => r.RoomNumber.Contains(keyword) ||
                           r.RoomDescription.Contains(keyword))
                .ToList();
        }

        // Check if room has any bookings (for delete logic)
        public bool HasBookings(int roomId)
        {
            using var context = new FUMiniHotelManagementContext();
            return context.BookingDetails.Any(bd => bd.RoomID == roomId);
        }
    }
}