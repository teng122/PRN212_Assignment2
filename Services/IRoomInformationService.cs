using BusinessObject;

namespace Services
{
    public interface IRoomInformationService
    {
        List<RoomInformation> GetAllRooms();
        RoomInformation? GetRoomById(int id);
        void AddRoom(RoomInformation room);
        void UpdateRoom(RoomInformation room);
        void DeleteRoom(int id);
        List<RoomInformation> SearchRooms(string keyword);
    }
}