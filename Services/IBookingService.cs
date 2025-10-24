using BusinessObject;

namespace Services
{
    public interface IBookingService
    {
        List<BookingReservation> GetAllBookings();
        List<BookingReservation> GetBookingsByDateRange(DateTime startDate, DateTime endDate);
        BookingReservation? GetBookingById(int id);
        void AddBooking(BookingReservation booking, List<BookingDetail> details);
        List<BookingDetail> GetBookingDetailsByReservationId(int reservationId);
        decimal CalculateTotalRevenue(DateTime startDate, DateTime endDate);
    }
}