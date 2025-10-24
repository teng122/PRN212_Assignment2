using BusinessObject;

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
            var rooms = DataProvider.Instance.Rooms;
            var roomTypes = DataProvider.Instance.RoomTypes;

            foreach (var room in rooms)
            {
                room.RoomType = roomTypes.FirstOrDefault(rt => rt.RoomTypeID == room.RoomTypeID);
            }

            return rooms;
        }

        public RoomInformation? GetRoomById(int id)
        {
            var room = DataProvider.Instance.Rooms.FirstOrDefault(r => r.RoomID == id);
            if (room != null)
            {
                room.RoomType = DataProvider.Instance.RoomTypes
                    .FirstOrDefault(rt => rt.RoomTypeID == room.RoomTypeID);
            }
            return room;
        }

        public void AddRoom(RoomInformation room)
        {
            room.RoomID = DataProvider.Instance.Rooms.Any()
                ? DataProvider.Instance.Rooms.Max(r => r.RoomID) + 1
                : 1;
            DataProvider.Instance.Rooms.Add(room);
        }

        public void UpdateRoom(RoomInformation room)
        {
            var existingRoom = GetRoomById(room.RoomID);
            if (existingRoom != null)
            {
                existingRoom.RoomNumber = room.RoomNumber;
                existingRoom.RoomDescription = room.RoomDescription;
                existingRoom.RoomMaxCapacity = room.RoomMaxCapacity;
                existingRoom.RoomStatus = room.RoomStatus;
                existingRoom.RoomPricePerDate = room.RoomPricePerDate;
                existingRoom.RoomTypeID = room.RoomTypeID;
            }
        }

        public void DeleteRoom(int id)
        {
            var room = GetRoomById(id);
            if (room != null)
            {
                room.RoomStatus = 2; // Soft delete
            }
        }

        public List<RoomInformation> SearchRooms(string keyword)
        {
            return GetRooms()
                .Where(r => r.RoomNumber.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                           r.RoomDescription.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}