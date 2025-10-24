// File: Views/CustomerMainWindow.xaml.cs
using System.Windows;
using System.Windows.Input;
using BusinessObject;
using Services;

namespace HotelManager.Views
{
    public partial class CustomerMainWindow : Window
    {
        private readonly ICustomerService _customerService;
        private readonly IBookingService _bookingService;
        private Customer _currentCustomer;

        public CustomerMainWindow(Customer customer)
        {
            InitializeComponent();
            _customerService = new CustomerService();
            _bookingService = new BookingService();
            _currentCustomer = customer;
            LoadCustomerProfile();
            LoadBookingHistory();
        }

        private void LoadCustomerProfile()
        {
            txtWelcome.Text = $"Welcome, {_currentCustomer.CustomerFullName}!";
            txtFullName.Text = _currentCustomer.CustomerFullName;
            txtTelephone.Text = _currentCustomer.Telephone;
            txtEmail.Text = _currentCustomer.EmailAddress;
            dpBirthday.SelectedDate = _currentCustomer.CustomerBirthday;
            txtPassword.Password = _currentCustomer.Password;
        }

        private void LoadBookingHistory()
        {
            try
            {
                // Get all bookings for current customer
                var allBookings = _bookingService.GetAllBookings();
                var customerBookings = allBookings
                    .Where(b => b.CustomerID == _currentCustomer.CustomerID)
                    .OrderByDescending(b => b.BookingDate)
                    .ToList();

                dgBookingHistory.ItemsSource = customerBookings;
                txtTotalCustomerBookings.Text = customerBookings.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading booking history: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdateProfile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtFullName.Text))
                {
                    MessageBox.Show("Full name is required.", "Validation Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtTelephone.Text))
                {
                    MessageBox.Show("Telephone is required.", "Validation Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPassword.Password))
                {
                    MessageBox.Show("Password is required.", "Validation Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                _currentCustomer.CustomerFullName = txtFullName.Text.Trim();
                _currentCustomer.Telephone = txtTelephone.Text.Trim();
                _currentCustomer.CustomerBirthday = dpBirthday.SelectedDate;
                _currentCustomer.Password = txtPassword.Password;

                _customerService.UpdateCustomer(_currentCustomer);

                MessageBox.Show("Profile updated successfully!", "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                txtWelcome.Text = $"Welcome, {_currentCustomer.CustomerFullName}!";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRefreshBooking_Click(object sender, RoutedEventArgs e)
        {
            LoadBookingHistory();
            MessageBox.Show("Booking history refreshed!", "Success",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void dgBookingHistory_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgBookingHistory.SelectedItem is BookingReservation selectedBooking)
            {
                ShowBookingDetails(selectedBooking);
            }
        }

        private void ShowBookingDetails(BookingReservation booking)
        {
            try
            {
                var details = _bookingService.GetBookingDetailsByReservationId(booking.BookingReservationID);

                string detailsMessage = $"Booking ID: {booking.BookingReservationID}\n";
                detailsMessage += $"Booking Date: {booking.BookingDate:dd/MM/yyyy}\n";
                detailsMessage += $"Total Price: {booking.TotalPrice:N0} VND\n";
                detailsMessage += $"Status: {(booking.BookingStatus == 1 ? "Active" : "Cancelled")}\n\n";
                detailsMessage += "Room Details:\n";
                detailsMessage += new string('-', 50) + "\n";

                if (details.Any())
                {
                    foreach (var detail in details)
                    {
                        detailsMessage += $"\n• Room: {detail.RoomInformation?.RoomNumber ?? "N/A"}\n";
                        detailsMessage += $"  Type: {detail.RoomInformation?.RoomType?.RoomTypeName ?? "N/A"}\n";
                        detailsMessage += $"  Check-in: {detail.StartDate:dd/MM/yyyy}\n";
                        detailsMessage += $"  Check-out: {detail.EndDate:dd/MM/yyyy}\n";
                        detailsMessage += $"  Price: {detail.ActualPrice:N0} VND\n";
                    }
                }
                else
                {
                    detailsMessage += "\nNo room details available.";
                }

                MessageBox.Show(detailsMessage, "Booking Details",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading booking details: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}