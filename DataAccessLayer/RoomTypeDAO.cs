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
            return DataProvider.Instance.RoomTypes;
        }

        public RoomType? GetRoomTypeById(int id)
        {
            return DataProvider.Instance.RoomTypes.FirstOrDefault(rt => rt.RoomTypeID == id);
        }
    }
}