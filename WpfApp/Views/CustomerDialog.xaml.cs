// File: Views/CustomerDialog.xaml.cs
using System.Windows;
using System.Windows.Controls;
using BusinessObject;

namespace HotelManager.Views
{
    public partial class CustomerDialog : Window
    {
        public Customer Customer { get; private set; }
        private bool isEditMode = false;

        public CustomerDialog()
        {
            InitializeComponent();
            Customer = new Customer { CustomerStatus = 1 };
            cmbStatus.SelectedIndex = 0;
        }

        public CustomerDialog(Customer customer)
        {
            InitializeComponent();
            Customer = customer;
            isEditMode = true;
            LoadCustomerData();
        }

        private void LoadCustomerData()
        {
            txtFullName.Text = Customer.CustomerFullName;
            txtTelephone.Text = Customer.Telephone;
            txtEmail.Text = Customer.EmailAddress;
            dpBirthday.SelectedDate = Customer.CustomerBirthday;
            txtPassword.Password = Customer.Password;
            cmbStatus.SelectedIndex = Customer.CustomerStatus - 1;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtFullName.Text))
                {
                    MessageBox.Show("Full name is required.", "Validation Error",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("Email is required.", "Validation Error",
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

                Customer.CustomerFullName = txtFullName.Text.Trim();
                Customer.Telephone = txtTelephone.Text.Trim();
                Customer.EmailAddress = txtEmail.Text.Trim();
                Customer.CustomerBirthday = dpBirthday.SelectedDate;
                Customer.Password = txtPassword.Password;
                Customer.CustomerStatus = int.Parse(((ComboBoxItem)cmbStatus.SelectedItem).Tag.ToString());

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