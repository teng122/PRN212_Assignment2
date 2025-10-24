using BusinessObject;
using Repositories;

namespace Services
{
    public class RoomInformationService : IRoomInformationService
    {
        private readonly IRoomInformationRepository _roomRepository;

        public RoomInformationService()
        {
            _roomRepository = new RoomInformationRepository();
        }

        public List<RoomInformation> GetAllRooms()
            => _roomRepository.GetRooms().Where(r => r.RoomStatus == 1).ToList();

        public RoomInformation? GetRoomById(int id)
            => _roomRepository.GetRoomById(id);

        public void AddRoom(RoomInformation room)
        {
            ValidateRoom(room);
            _roomRepository.AddRoom(room);
        }

        public void UpdateRoom(RoomInformation room)
        {
            ValidateRoom(room);
            _roomRepository.UpdateRoom(room);
        }

        public void DeleteRoom(int id)
            => _roomRepository.DeleteRoom(id);

        public List<RoomInformation> SearchRooms(string keyword)
            => _roomRepository.SearchRooms(keyword).Where(r => r.RoomStatus == 1).ToList();

        private void ValidateRoom(RoomInformation room)
        {
            if (string.IsNullOrWhiteSpace(room.RoomNumber))
                throw new Exception("Room number is required");

            if (room.RoomNumber.Length > 50)
                throw new Exception("Room number must not exceed 50 characters");

            if (room.RoomDescription.Length > 220)
                throw new Exception("Room description must not exceed 220 characters");

            if (room.RoomMaxCapacity <= 0)
                throw new Exception("Room max capacity must be greater than 0");

            if (room.RoomPricePerDate <= 0)
                throw new Exception("Room price must be greater than 0");
        }
    }
}