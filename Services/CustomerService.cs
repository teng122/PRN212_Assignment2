using BusinessObject;
using Repositories;

namespace Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService()
        {
            _customerRepository = new CustomerRepository();
        }

        public List<Customer> GetAllCustomers()
            => _customerRepository.GetCustomers().Where(c => c.CustomerStatus == 1).ToList();

        public Customer? GetCustomerById(int id)
            => _customerRepository.GetCustomerById(id);

        public Customer? GetCustomerByEmail(string email)
            => _customerRepository.GetCustomerByEmail(email);

        public void AddCustomer(Customer customer)
        {
            ValidateCustomer(customer);
            _customerRepository.AddCustomer(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            ValidateCustomer(customer);
            _customerRepository.UpdateCustomer(customer);
        }

        public void DeleteCustomer(int id)
            => _customerRepository.DeleteCustomer(id);

        public List<Customer> SearchCustomers(string keyword)
            => _customerRepository.SearchCustomers(keyword).Where(c => c.CustomerStatus == 1).ToList();

        private void ValidateCustomer(Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.CustomerFullName))
                throw new Exception("Customer name is required");

            if (string.IsNullOrWhiteSpace(customer.EmailAddress))
                throw new Exception("Email is required");

            if (string.IsNullOrWhiteSpace(customer.Telephone))
                throw new Exception("Telephone is required");

            if (customer.CustomerFullName.Length > 50)
                throw new Exception("Customer name must not exceed 50 characters");

            if (customer.Telephone.Length > 12)
                throw new Exception("Telephone must not exceed 12 characters");

            if (customer.EmailAddress.Length > 50)
                throw new Exception("Email must not exceed 50 characters");
        }
    }
}