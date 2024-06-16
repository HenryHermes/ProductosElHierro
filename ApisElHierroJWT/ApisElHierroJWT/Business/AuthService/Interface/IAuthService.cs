using ApisElHierroJWT.Models;

namespace ApisElHierroJWT.Business.AuthService.Interface
{
    public interface IAuthService
    {
        public Task<string> Login(string email, string password);
        
    }
}
