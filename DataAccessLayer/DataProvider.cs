using BusinessObject;

namespace DAO
{
    public class DataProvider
    {
        private static DataProvider? _instance;
        private static readonly object _lock = new object();

        public List<Customer> Customers { get; set; }
        public List<RoomType> RoomTypes { get; set; }
        public List<RoomInformation> Rooms { get; set; }
        public List<BookingReservation> BookingReservations { get; set; }
        public List<BookingDetail> BookingDetails { get; set; }

        private DataProvider()
        {
            InitializeData();
        }

        public static DataProvider Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new DataProvider();
                        }
                    }
                }
                return _instance;
            }
        }

        private void InitializeData()
        {
            // Initialize RoomTypes
            RoomTypes = new List<RoomType>
            {
                new RoomType { RoomTypeID = 1, RoomTypeName = "Standard", TypeDescription = "Standard Room", TypeNote = "Basic amenities" },
                new RoomType { RoomTypeID = 2, RoomTypeName = "Deluxe", TypeDescription = "Deluxe Room", TypeNote = "Premium amenities" },
                new RoomType { RoomTypeID = 3, RoomTypeName = "Suite", TypeDescription = "Suite Room", TypeNote = "Luxury amenities" }
            };

            // Initialize Customers
            Customers = new List<Customer>
            {
                new Customer
                {
                    CustomerID = 1,
                    CustomerFullName = "Nguyen Van A",
                    Telephone = "0901234567",
                    EmailAddress = "nguyenvana@gmail.com",
                    CustomerBirthday = new DateTime(1990, 5, 15),
                    CustomerStatus = 1,
                    Password = "123456"
                },
                new Customer
                {
                    CustomerID = 2,
                    CustomerFullName = "Tran Thi B",
                    Telephone = "0912345678",
                    EmailAddress = "tranthib@gmail.com",
                    CustomerBirthday = new DateTime(1995, 8, 20),
                    CustomerStatus = 1,
                    Password = "123456"
                },
                new Customer
                {
                    CustomerID = 3,
                    CustomerFullName = "Le Van C",
                    Telephone = "0923456789",
                    EmailAddress = "levanc@gmail.com",
                    CustomerBirthday = new DateTime(1988, 3, 10),
                    CustomerStatus = 1,
                    Password = "123456"
                }
            };

            // Initialize Rooms
            Rooms = new List<RoomInformation>
            {
                new RoomInformation
                {
                    RoomID = 1,
                    RoomNumber = "101",
                    RoomDescription = "Standard room with city view",
                    RoomMaxCapacity = 2,
                    RoomStatus = 1,
                    RoomPricePerDate = 500000,
                    RoomTypeID = 1
                },
                new RoomInformation
                {
                    RoomID = 2,
                    RoomNumber = "102",
                    RoomDescription = "Deluxe room with sea view",
                    RoomMaxCapacity = 3,
                    RoomStatus = 1,
                    RoomPricePerDate = 800000,
                    RoomTypeID = 2
                },
                new RoomInformation
                {
                    RoomID = 3,
                    RoomNumber = "201",
                    RoomDescription = "Suite with balcony",
                    RoomMaxCapacity = 4,
                    RoomStatus = 1,
                    RoomPricePerDate = 1200000,
                    RoomTypeID = 3
                }
            };

            // Initialize Booking Reservations (Sample data)
            BookingReservations = new List<BookingReservation>
            {
                new BookingReservation
                {
                    BookingReservationID = 1,
                    BookingDate = new DateTime(2025, 1, 15),
                    TotalPrice = 1500000,
                    CustomerID = 1,
                    BookingStatus = 1
                },
                new BookingReservation
                {
                    BookingReservationID = 2,
                    BookingDate = new DateTime(2025, 2, 20),
                    TotalPrice = 2400000,
                    CustomerID = 2,
                    BookingStatus = 1
                },
                new BookingReservation
                {
                    BookingReservationID = 3,
                    BookingDate = new DateTime(2025, 3, 10),
                    TotalPrice = 3600000,
                    CustomerID = 3,
                    BookingStatus = 1
                },
                new BookingReservation
                {
                    BookingReservationID = 4,
                    BookingDate = new DateTime(2025, 4, 5),
                    TotalPrice = 1000000,
                    CustomerID = 1,
                    BookingStatus = 1
                },
                new BookingReservation
                {
                    BookingReservationID = 5,
                    BookingDate = new DateTime(2025, 5, 12),
                    TotalPrice = 1600000,
                    CustomerID = 2,
                    BookingStatus = 1
                }
            };

            // Initialize Booking Details
            BookingDetails = new List<BookingDetail>
            {
                new BookingDetail
                {
                    BookingReservationID = 1,
                    RoomID = 1,
                    StartDate = new DateTime(2025, 1, 15),
                    EndDate = new DateTime(2025, 1, 18),
                    ActualPrice = 1500000
                },
                new BookingDetail
                {
                    BookingReservationID = 2,
                    RoomID = 2,
                    StartDate = new DateTime(2025, 2, 20),
                    EndDate = new DateTime(2025, 2, 23),
                    ActualPrice = 2400000
                },
                new BookingDetail
                {
                    BookingReservationID = 3,
                    RoomID = 3,
                    StartDate = new DateTime(2025, 3, 10),
                    EndDate = new DateTime(2025, 3, 13),
                    ActualPrice = 3600000
                },
                new BookingDetail
                {
                    BookingReservationID = 4,
                    RoomID = 1,
                    StartDate = new DateTime(2025, 4, 5),
                    EndDate = new DateTime(2025, 4, 7),
                    ActualPrice = 1000000
                },
                new BookingDetail
                {
                    BookingReservationID = 5,
                    RoomID = 2,
                    StartDate = new DateTime(2025, 5, 12),
                    EndDate = new DateTime(2025, 5, 14),
                    ActualPrice = 1600000
                }
            };
        }
    }
}