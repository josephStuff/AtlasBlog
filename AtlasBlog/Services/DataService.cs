using AtlasBlog.Data;
using Microsoft.EntityFrameworkCore;

namespace AtlasBlog.Services
{
    public class DataService
    {
        // ------------ CALLING A METHOD OR INSTRUCTION THAT EXECUTES THE MIGRATIONS INSTRUCTIONS ----------
        readonly ApplicationDbContext _dbContext;

        public DataService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task SetupDb()
        {
            // ---------------  _RUN THE MIGRATIONS ASYNC --------------
            await _dbContext.Database.MigrateAsync();
        }
    }
}
