using Microsoft.EntityFrameworkCore;

namespace Selu383.SP24.Api
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
        }
    }
}
