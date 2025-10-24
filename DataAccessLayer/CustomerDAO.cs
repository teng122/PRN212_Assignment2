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
            using var context = new FUMiniHotelManagementContext();
            return context.Customers.ToList();
        }

        public Customer? GetCustomerById(int id)
        {
            using var context = new FUMiniHotelManagementContext();
            return context.Customers.FirstOrDefault(c => c.CustomerID == id);
        }

        public Customer? GetCustomerByEmail(string email)
        {
            using var context = new FUMiniHotelManagementContext();
            return context.Customers.FirstOrDefault(c => c.EmailAddress == email);
        }

        public void AddCustomer(Customer customer)
        {
            using var context = new FUMiniHotelManagementContext();
            context.Customers.Add(customer);
            context.SaveChanges();
        }

        public void UpdateCustomer(Customer customer)
        {
            using var context = new FUMiniHotelManagementContext();
            context.Customers.Update(customer);
            context.SaveChanges();
        }

        public void DeleteCustomer(int id)
        {
            using var context = new FUMiniHotelManagementContext();
            var customer = context.Customers.FirstOrDefault(c => c.CustomerID == id);
            if (customer != null)
            {
                customer.CustomerStatus = 2; // Soft delete
                context.SaveChanges();
            }
        }

        public List<Customer> SearchCustomers(string keyword)
        {
            using var context = new FUMiniHotelManagementContext();
            return context.Customers
                .Where(c => c.CustomerFullName.Contains(keyword) ||
                           c.EmailAddress.Contains(keyword) ||
                           c.Telephone.Contains(keyword))
                .ToList();
        }
    }
}