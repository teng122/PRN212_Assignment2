using System.Windows;
using System.Windows.Input;
using Services;

namespace HotelManager.Views
{
    public partial class LoginWindow : Window
    {
        private readonly IAuthenticationService _authService;

        public LoginWindow()
        {
            InitializeComponent();
            _authService = new AuthenticationService(App.Configuration);
            txtPassword.Password = "@@abc123@@"; // For testing - remove in production
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            PerformLogin();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PerformLogin();
            }
        }

        private void PerformLogin()
        {
            try
            {
                txtError.Text = string.Empty;
                string email = txtEmail.Text.Trim();
                string password = txtPassword.Password;

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    txtError.Text = "Please enter email and password";
                    return;
                }

                var (success, role, customer) = _authService.Login(email, password);

                if (success)
                {
                    if (role == "Admin")
                    {
                        AdminMainWindow adminWindow = new AdminMainWindow();
                        adminWindow.Show();
                    }
                    else if (role == "Customer")
                    {
                        CustomerMainWindow customerWindow = new CustomerMainWindow(customer);
                        customerWindow.Show();
                    }
                    this.Close();
                }
                else
                {
                    txtError.Text = "Invalid email or password";
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Login Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}