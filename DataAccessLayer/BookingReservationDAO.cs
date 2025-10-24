using BusinessObject;
using Microsoft.EntityFrameworkCore;

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
            using var context = new FUMiniHotelManagementContext();
            return context.BookingReservations
                .Include(b => b.Customer)
                .ToList();
        }

        public List<BookingReservation> GetBookingsByDateRange(DateTime startDate, DateTime endDate)
        {
            using var context = new FUMiniHotelManagementContext();
            return context.BookingReservations
                .Include(b => b.Customer)
                .Where(b => b.BookingDate >= startDate && b.BookingDate <= endDate)
                .OrderByDescending(b => b.TotalPrice)
                .ToList();
        }

        public BookingReservation? GetBookingById(int id)
        {
            using var context = new FUMiniHotelManagementContext();
            return context.BookingReservations
                .Include(b => b.Customer)
                .FirstOrDefault(b => b.BookingReservationID == id);
        }

        public void AddBooking(BookingReservation booking)
        {
            using var context = new FUMiniHotelManagementContext();

            // Generate new ID manually (since IDENTITY is not used in DB)
            var maxId = context.BookingReservations.Any()
                ? context.BookingReservations.Max(b => b.BookingReservationID)
                : 0;
            booking.BookingReservationID = maxId + 1;

            context.BookingReservations.Add(booking);
            context.SaveChanges();
        }

        public void UpdateBooking(BookingReservation booking)
        {
            using var context = new FUMiniHotelManagementContext();
            context.BookingReservations.Update(booking);
            context.SaveChanges();
        }

        public void DeleteBooking(int id)
        {
            using var context = new FUMiniHotelManagementContext();
            var booking = context.BookingReservations.FirstOrDefault(b => b.BookingReservationID == id);
            if (booking != null)
            {
                context.BookingReservations.Remove(booking);
                context.SaveChanges();
            }
        }
    }
}