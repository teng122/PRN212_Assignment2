using BusinessObject;

namespace Services
{
    public interface IAuthenticationService
    {
        (bool success, string role, Customer? customer) Login(string email, string password);
    }
}