using BusinessObject;

namespace Repositories
{
    public interface IRoomInformationRepository
    {
        List<RoomInformation> GetRooms();
        RoomInformation? GetRoomById(int id);
        void AddRoom(RoomInformation room);
        void UpdateRoom(RoomInformation room);
        void DeleteRoom(int id);
        List<RoomInformation> SearchRooms(string keyword);
    }
}