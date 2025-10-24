// File: Views/RoomDialog.xaml.cs
using System.Windows;
using System.Windows.Controls;
using BusinessObject;
using Services;

namespace HotelManager.Views
{
    public partial class RoomDialog : Window
    {
        public RoomInformation Room { get; private set; }
        private readonly IRoomTypeService _roomTypeService;
        private bool isEditMode = false;

        public RoomDialog()
        {
            InitializeComponent();
            _roomTypeService = new RoomTypeService();
            Room = new RoomInformation { RoomStatus = 1 };
            LoadRoomTypes();
            cmbStatus.SelectedIndex = 0;
        }

        public RoomDialog(RoomInformation room)
        {
            InitializeComponent();
            _roomTypeService = new RoomTypeService();
            Room = room;
            isEditMode = true;
            LoadRoomTypes();
            LoadRoomData();
        }

        private void LoadRoomTypes()
        {
            cmbRoomType.ItemsSource = _roomTypeService.GetAllRoomTypes();
        }

        private void LoadRoomData()
        {
            txtRoomNumber.Text = Room.RoomNumber;
            txtDescription.Text = Room.RoomDescription;
            txtMaxCapacity.Text = Room.RoomMaxCapacity.ToString();
            txtPrice.Text = Room.RoomPricePerDate.ToString();
            cmbRoomType.SelectedValue = Room.RoomTypeID;
            cmbStatus.SelectedIndex = Room.RoomStatus - 1;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtRoomNumber.Text))
                {
                    MessageBox.Show("Room number is required.", "Validation Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!int.TryParse(txtMaxCapacity.Text, out int maxCapacity) || maxCapacity <= 0)
                {
                    MessageBox.Show("Max capacity must be a positive number.", "Validation Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!decimal.TryParse(txtPrice.Text, out decimal price) || price <= 0)
                {
                    MessageBox.Show("Price must be a positive number.", "Validation Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (cmbRoomType.SelectedValue == null)
                {
                    MessageBox.Show("Please select a room type.", "Validation Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Room.RoomNumber = txtRoomNumber.Text.Trim();
                Room.RoomDescription = txtDescription.Text.Trim();
                Room.RoomMaxCapacity = maxCapacity;
                Room.RoomPricePerDate = price;
                Room.RoomTypeID = (int)cmbRoomType.SelectedValue;
                Room.RoomStatus = int.Parse(((ComboBoxItem)cmbStatus.SelectedItem).Tag.ToString());

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}