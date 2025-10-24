using BusinessObject;
using DAO;

namespace Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public List<Customer> GetCustomers() => CustomerDAO.Instance.GetCustomers();

        public Customer? GetCustomerById(int id) => CustomerDAO.Instance.GetCustomerById(id);

        public Customer? GetCustomerByEmail(string email) => CustomerDAO.Instance.GetCustomerByEmail(email);

        public void AddCustomer(Customer customer) => CustomerDAO.Instance.AddCustomer(customer);

        public void UpdateCustomer(Customer customer) => CustomerDAO.Instance.UpdateCustomer(customer);

        public void DeleteCustomer(int id) => CustomerDAO.Instance.DeleteCustomer(id);

        public List<Customer> SearchCustomers(string keyword) => CustomerDAO.Instance.SearchCustomers(keyword);
    }
}