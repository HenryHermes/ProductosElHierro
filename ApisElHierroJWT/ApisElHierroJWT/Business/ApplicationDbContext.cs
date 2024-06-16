using ApisElHierroJWT.Models;
using Microsoft.EntityFrameworkCore;

namespace ApisElHierroJWT.Business
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

    }
}
