namespace BusinessObject
{
    public class BookingReservation
    {
        public int BookingReservationID { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int CustomerID { get; set; }
        public int BookingStatus { get; set; }
        public Customer? Customer { get; set; }
    }
}