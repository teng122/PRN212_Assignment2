using BusinessObject;
using DAO;

namespace Repositories
{
    public class BookingRepository : IBookingRepository
    {
        public List<BookingReservation> GetAllBookings()
            => BookingReservationDAO.Instance.GetAllBookings();

        public List<BookingReservation> GetBookingsByDateRange(DateTime startDate, DateTime endDate)
            => BookingReservationDAO.Instance.GetBookingsByDateRange(startDate, endDate);

        public BookingReservation? GetBookingById(int id)
            => BookingReservationDAO.Instance.GetBookingById(id);

        public void AddBooking(BookingReservation booking)
            => BookingReservationDAO.Instance.AddBooking(booking);

        public List<BookingDetail> GetBookingDetailsByReservationId(int reservationId)
            => BookingDetailDAO.Instance.GetBookingDetailsByReservationId(reservationId);
    }
}