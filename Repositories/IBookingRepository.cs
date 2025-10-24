using BusinessObject;

namespace Repositories
{
    public interface IBookingRepository
    {
        List<BookingReservation> GetAllBookings();
        List<BookingReservation> GetBookingsByDateRange(DateTime startDate, DateTime endDate);
        BookingReservation? GetBookingById(int id);
        void AddBooking(BookingReservation booking);
        List<BookingDetail> GetBookingDetailsByReservationId(int reservationId);
    }
}