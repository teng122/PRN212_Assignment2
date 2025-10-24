using BusinessObject;

namespace DAO
{
    public class RoomTypeDAO
    {
        private static RoomTypeDAO? _instance;
        private static readonly object _lock = new object();

        public static RoomTypeDAO Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new RoomTypeDAO();
                        }
                    }
                }
                return _instance;
            }
        }

        public List<RoomType> GetRoomTypes()
        {
            using var context = new FUMiniHotelManagementContext();
            return context.RoomTypes.ToList();
        }

        public RoomType? GetRoomTypeById(int id)
        {
            using var context = new FUMiniHotelManagementContext();
            return context.RoomTypes.FirstOrDefault(rt => rt.RoomTypeID == id);
        }
    }
}