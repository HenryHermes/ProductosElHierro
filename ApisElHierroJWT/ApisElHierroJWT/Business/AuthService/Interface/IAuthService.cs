using ApisElHierroJWT.Models;

namespace ApisElHierroJWT.Business.AuthService.Interface
{
    public interface IAuthService
    {
        public Task<TokenReturn> Login(string email, string password);
        
    }
}
