using BusinessObject;
using DAO;

namespace Repositories
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        public List<RoomType> GetRoomTypes() => RoomTypeDAO.Instance.GetRoomTypes();

        public RoomType? GetRoomTypeById(int id) => RoomTypeDAO.Instance.GetRoomTypeById(id);
    }
}