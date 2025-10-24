using BusinessObject;
using Repositories;

namespace Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService()
        {
            _bookingRepository = new BookingRepository();
        }

        public List<BookingReservation> GetAllBookings()
            => _bookingRepository.GetAllBookings();

        public List<BookingReservation> GetBookingsByDateRange(DateTime startDate, DateTime endDate)
            => _bookingRepository.GetBookingsByDateRange(startDate, endDate);

        public BookingReservation? GetBookingById(int id)
            => _bookingRepository.GetBookingById(id);

        public void AddBooking(BookingReservation booking, List<BookingDetail> details)
        {
            _bookingRepository.AddBooking(booking);

            foreach (var detail in details)
            {
                detail.BookingReservationID = booking.BookingReservationID;
            }
        }

        public List<BookingDetail> GetBookingDetailsByReservationId(int reservationId)
            => _bookingRepository.GetBookingDetailsByReservationId(reservationId);

        public decimal CalculateTotalRevenue(DateTime startDate, DateTime endDate)
        {
            var bookings = GetBookingsByDateRange(startDate, endDate);
            return bookings.Sum(b => b.TotalPrice);
        }
    }
}