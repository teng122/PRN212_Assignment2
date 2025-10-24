using BusinessObject;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ICustomerService _customerService;
        private readonly IConfiguration _configuration;

        public AuthenticationService(IConfiguration configuration)
        {
            _customerService = new CustomerService();
            _configuration = configuration;
        }

        public (bool success, string role, Customer? customer) Login(string email, string password)
        {
            // Check admin credentials from appsettings.json
            var adminEmail = _configuration["AdminAccount:Email"];
            var adminPassword = _configuration["AdminAccount:Password"];

            if (email == adminEmail && password == adminPassword)
            {
                return (true, "Admin", null);
            }

            // Check customer credentials
            var customer = _customerService.GetCustomerByEmail(email);
            if (customer != null && customer.Password == password && customer.CustomerStatus == 1)
            {
                return (true, "Customer", customer);
            }

            return (false, string.Empty, null);
        }
    }
}