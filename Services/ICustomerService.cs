﻿using BusinessObject;

namespace Services
{
    public interface ICustomerService
    {
        List<Customer> GetAllCustomers();
        Customer? GetCustomerById(int id);
        Customer? GetCustomerByEmail(string email);
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int id);
        List<Customer> SearchCustomers(string keyword);
    }
}