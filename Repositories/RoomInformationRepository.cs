using BusinessObject;
using DAO;

namespace Repositories
{
    public class RoomInformationRepository : IRoomInformationRepository
    {
        public List<RoomInformation> GetRooms() => RoomInformationDAO.Instance.GetRooms();

        public RoomInformation? GetRoomById(int id) => RoomInformationDAO.Instance.GetRoomById(id);

        public void AddRoom(RoomInformation room) => RoomInformationDAO.Instance.AddRoom(room);

        public void UpdateRoom(RoomInformation room) => RoomInformationDAO.Instance.UpdateRoom(room);

        public void DeleteRoom(int id) => RoomInformationDAO.Instance.DeleteRoom(id);

        public List<RoomInformation> SearchRooms(string keyword) => RoomInformationDAO.Instance.SearchRooms(keyword);
    }
}