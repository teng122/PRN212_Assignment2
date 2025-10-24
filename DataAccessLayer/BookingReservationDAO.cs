using BusinessObject;

namespace DAO
{
    public class BookingReservationDAO
    {
        private static BookingReservationDAO? _instance;
        private static readonly object _lock = new object();

        public static BookingReservationDAO Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new BookingReservationDAO();
                        }
                    }
                }
                return _instance;
            }
        }

        public List<BookingReservation> GetAllBookings()
        {
            var bookings = DataProvider.Instance.BookingReservations;
            var customers = DataProvider.Instance.Customers;

            foreach (var booking in bookings)
            {
                booking.Customer = customers.FirstOrDefault(c => c.CustomerID == booking.CustomerID);
            }

            return bookings;
        }

        public List<BookingReservation> GetBookingsByDateRange(DateTime startDate, DateTime endDate)
        {
            var bookings = DataProvider.Instance.BookingReservations
                .Where(b => b.BookingDate.Date >= startDate.Date && b.BookingDate.Date <= endDate.Date)
                .OrderByDescending(b => b.TotalPrice)
                .ToList();

            var customers = DataProvider.Instance.Customers;

            foreach (var booking in bookings)
            {
                booking.Customer = customers.FirstOrDefault(c => c.CustomerID == booking.CustomerID);
            }

            return bookings;
        }

        public BookingReservation? GetBookingById(int id)
        {
            var booking = DataProvider.Instance.BookingReservations
                .FirstOrDefault(b => b.BookingReservationID == id);

            if (booking != null)
            {
                booking.Customer = DataProvider.Instance.Customers
                    .FirstOrDefault(c => c.CustomerID == booking.CustomerID);
            }

            return booking;
        }

        public void AddBooking(BookingReservation booking)
        {
            booking.BookingReservationID = DataProvider.Instance.BookingReservations.Any()
                ? DataProvider.Instance.BookingReservations.Max(b => b.BookingReservationID) + 1
                : 1;
            DataProvider.Instance.BookingReservations.Add(booking);
        }
    }
}