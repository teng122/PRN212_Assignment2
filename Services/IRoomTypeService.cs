using BusinessObject;

namespace Services
{
    public interface IRoomTypeService
    {
        List<RoomType> GetAllRoomTypes();
        RoomType? GetRoomTypeById(int id);
    }
}
