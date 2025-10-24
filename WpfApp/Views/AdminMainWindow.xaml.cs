// File: Views/AdminMainWindow.xaml.cs
using System.Windows;
using System.Windows.Controls;
using BusinessObject;
using Services;

namespace HotelManager.Views
{
    public partial class AdminMainWindow : Window
    {
        private readonly ICustomerService _customerService;
        private readonly IRoomInformationService _roomService;
        private readonly IBookingService _bookingService;

        public AdminMainWindow()
        {
            InitializeComponent();
            _customerService = new CustomerService();
            _roomService = new RoomInformationService();
            _bookingService = new BookingService();
            LoadCustomers();
            LoadRooms();
            InitializeReportDates();
        }

        private void InitializeReportDates()
        {
            // Set default date range to current month
            dpStartDate.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dpEndDate.SelectedDate = DateTime.Now;
        }

        private void LoadCustomers()
        {
            dgCustomers.ItemsSource = _customerService.GetAllCustomers();
        }

        private void LoadRooms()
        {
            dgRooms.ItemsSource = _roomService.GetAllRooms();
        }

        // Customer Management
        private void btnSearchCustomer_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtSearchCustomer.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadCustomers();
            }
            else
            {
                dgCustomers.ItemsSource = _customerService.SearchCustomers(keyword);
            }
        }

        private void btnRefreshCustomer_Click(object sender, RoutedEventArgs e)
        {
            txtSearchCustomer.Clear();
            LoadCustomers();
        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            CustomerDialog dialog = new CustomerDialog();
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    _customerService.AddCustomer(dialog.Customer);
                    MessageBox.Show("Customer added successfully!", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadCustomers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnUpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (dgCustomers.SelectedItem is Customer selectedCustomer)
            {
                CustomerDialog dialog = new CustomerDialog(selectedCustomer);
                if (dialog.ShowDialog() == true)
                {
                    try
                    {
                        _customerService.UpdateCustomer(dialog.Customer);
                        MessageBox.Show("Customer updated successfully!", "Success",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadCustomers();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Error",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a customer to update.", "Warning",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (dgCustomers.SelectedItem is Customer selectedCustomer)
            {
                var result = MessageBox.Show(
                    $"Are you sure you want to delete customer '{selectedCustomer.CustomerFullName}'?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _customerService.DeleteCustomer(selectedCustomer.CustomerID);
                        MessageBox.Show("Customer deleted successfully!", "Success",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadCustomers();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Error",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a customer to delete.", "Warning",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Room Management
        private void btnSearchRoom_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtSearchRoom.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadRooms();
            }
            else
            {
                dgRooms.ItemsSource = _roomService.SearchRooms(keyword);
            }
        }

        private void btnRefreshRoom_Click(object sender, RoutedEventArgs e)
        {
            txtSearchRoom.Clear();
            LoadRooms();
        }

        private void btnAddRoom_Click(object sender, RoutedEventArgs e)
        {
            RoomDialog dialog = new RoomDialog();
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    _roomService.AddRoom(dialog.Room);
                    MessageBox.Show("Room added successfully!", "Success",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadRooms();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnUpdateRoom_Click(object sender, RoutedEventArgs e)
        {
            if (dgRooms.SelectedItem is RoomInformation selectedRoom)
            {
                RoomDialog dialog = new RoomDialog(selectedRoom);
                if (dialog.ShowDialog() == true)
                {
                    try
                    {
                        _roomService.UpdateRoom(dialog.Room);
                        MessageBox.Show("Room updated successfully!", "Success",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadRooms();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Error",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a room to update.", "Warning",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnDeleteRoom_Click(object sender, RoutedEventArgs e)
        {
            if (dgRooms.SelectedItem is RoomInformation selectedRoom)
            {
                var result = MessageBox.Show(
                    $"Are you sure you want to delete room '{selectedRoom.RoomNumber}'?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _roomService.DeleteRoom(selectedRoom.RoomID);
                        MessageBox.Show("Room deleted successfully!", "Success",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadRooms();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Error",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a room to delete.", "Warning",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Booking Report
        private void btnGenerateReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!dpStartDate.SelectedDate.HasValue || !dpEndDate.SelectedDate.HasValue)
                {
                    MessageBox.Show("Please select both start date and end date.", "Validation Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                DateTime startDate = dpStartDate.SelectedDate.Value;
                DateTime endDate = dpEndDate.SelectedDate.Value;

                if (startDate > endDate)
                {
                    MessageBox.Show("Start date must be before or equal to end date.", "Validation Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Get bookings for the date range (sorted by TotalPrice descending)
                var bookings = _bookingService.GetBookingsByDateRange(startDate, endDate);
                dgBookingReport.ItemsSource = bookings;

                // Calculate statistics
                int totalBookings = bookings.Count;
                decimal totalRevenue = _bookingService.CalculateTotalRevenue(startDate, endDate);
                decimal averageValue = totalBookings > 0 ? totalRevenue / totalBookings : 0;

                // Update summary
                txtTotalBookings.Text = totalBookings.ToString();
                txtTotalRevenue.Text = $"{totalRevenue:N0} VND";
                txtAverageValue.Text = $"{averageValue:N0} VND";

                if (totalBookings == 0)
                {
                    MessageBox.Show("No bookings found for the selected date range.", "Information",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating report: {ex.Message}", "Error",
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