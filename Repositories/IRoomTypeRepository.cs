using BusinessObject;

namespace Repositories
{
    public interface IRoomTypeRepository
    {
        List<RoomType> GetRoomTypes();
        RoomType? GetRoomTypeById(int id);
    }
}