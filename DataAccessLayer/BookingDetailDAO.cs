using BusinessObject;
using Microsoft.EntityFrameworkCore;

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
            using var context = new FUMiniHotelManagementContext();
            return context.BookingDetails
                .Include(bd => bd.RoomInformation)
                    .ThenInclude(r => r.RoomType)
                .Include(bd => bd.BookingReservation)
                .Where(bd => bd.BookingReservationID == reservationId)
                .ToList();
        }

        public void AddBookingDetail(BookingDetail detail)
        {
            using var context = new FUMiniHotelManagementContext();
            context.BookingDetails.Add(detail);
            context.SaveChanges();
        }

        public void UpdateBookingDetail(BookingDetail detail)
        {
            using var context = new FUMiniHotelManagementContext();
            context.BookingDetails.Update(detail);
            context.SaveChanges();
        }

        public void DeleteBookingDetail(int bookingReservationId, int roomId)
        {
            using var context = new FUMiniHotelManagementContext();
            var detail = context.BookingDetails
                .FirstOrDefault(bd => bd.BookingReservationID == bookingReservationId
                                   && bd.RoomID == roomId);
            if (detail != null)
            {
                context.BookingDetails.Remove(detail);
                context.SaveChanges();
            }
        }

        public List<BookingDetail> GetAllBookingDetails()
        {
            using var context = new FUMiniHotelManagementContext();
            return context.BookingDetails
                .Include(bd => bd.RoomInformation)
                    .ThenInclude(r => r.RoomType)
                .Include(bd => bd.BookingReservation)
                    .ThenInclude(br => br.Customer)
                .ToList();
        }
    }
}