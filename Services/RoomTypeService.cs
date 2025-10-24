using BusinessObject;
using Repositories;

namespace Services
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly IRoomTypeRepository _roomTypeRepository;

        public RoomTypeService()
        {
            _roomTypeRepository = new RoomTypeRepository();
        }

        public List<RoomType> GetAllRoomTypes()
            => _roomTypeRepository.GetRoomTypes();

        public RoomType? GetRoomTypeById(int id)
            => _roomTypeRepository.GetRoomTypeById(id);
    }
}