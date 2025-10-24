using BusinessObject;

namespace DAO
{
    public class CustomerDAO
    {
        private static CustomerDAO? _instance;
        private static readonly object _lock = new object();

        public static CustomerDAO Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new CustomerDAO();
                        }
                    }
                }
                return _instance;
            }
        }

        public List<Customer> GetCustomers()
        {
            return DataProvider.Instance.Customers;
        }

        public Customer? GetCustomerById(int id)
        {
            return DataProvider.Instance.Customers.FirstOrDefault(c => c.CustomerID == id);
        }

        public Customer? GetCustomerByEmail(string email)
        {
            return DataProvider.Instance.Customers.FirstOrDefault(c => c.EmailAddress == email);
        }

        public void AddCustomer(Customer customer)
        {
            customer.CustomerID = DataProvider.Instance.Customers.Any()
                ? DataProvider.Instance.Customers.Max(c => c.CustomerID) + 1
                : 1;
            DataProvider.Instance.Customers.Add(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            var existingCustomer = GetCustomerById(customer.CustomerID);
            if (existingCustomer != null)
            {
                existingCustomer.CustomerFullName = customer.CustomerFullName;
                existingCustomer.Telephone = customer.Telephone;
                existingCustomer.EmailAddress = customer.EmailAddress;
                existingCustomer.CustomerBirthday = customer.CustomerBirthday;
                existingCustomer.CustomerStatus = customer.CustomerStatus;
                existingCustomer.Password = customer.Password;
            }
        }

        public void DeleteCustomer(int id)
        {
            var customer = GetCustomerById(id);
            if (customer != null)
            {
                customer.CustomerStatus = 2; // Soft delete
            }
        }

        public List<Customer> SearchCustomers(string keyword)
        {
            return DataProvider.Instance.Customers
                .Where(c => c.CustomerFullName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                           c.EmailAddress.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                           c.Telephone.Contains(keyword))
                .ToList();
        }
    }
}