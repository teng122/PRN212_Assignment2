using BusinessObject;

namespace DAO
{
    public class BookingDetailDAO
    {
        private static BookingDetailDAO? _instance;
        private static readonly object _lock = new object();

        public static BookingDetailDAO Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new BookingDetailDAO();
                        }
                    }
                }
                return _instance;
            }
        }

        public List<BookingDetail> GetBookingDetailsByReservationId(int reservationId)
        {
            var details = DataProvider.Instance.BookingDetails
                .Where(bd => bd.BookingReservationID == reservationId)
                .ToList();

            var rooms = DataProvider.Instance.Rooms;
            var roomTypes = DataProvider.Instance.RoomTypes;

            foreach (var detail in details)
            {
                detail.RoomInformation = rooms.FirstOrDefault(r => r.RoomID == detail.RoomID);
                if (detail.RoomInformation != null)
                {
                    detail.RoomInformation.RoomType = roomTypes
                        .FirstOrDefault(rt => rt.RoomTypeID == detail.RoomInformation.RoomTypeID);
                }
            }

            return details;
        }

        public void AddBookingDetail(BookingDetail detail)
        {
            DataProvider.Instance.BookingDetails.Add(detail);
        }
    }
}