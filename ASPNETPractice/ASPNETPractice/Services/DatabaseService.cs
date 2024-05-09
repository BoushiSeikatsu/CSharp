using ASPNETPractice.Database;
using ASPNETPractice.Models;

namespace ASPNETPractice.Services
{
    public class DatabaseService
    {
        private readonly DataLayer _dataLayer;

        public DatabaseService()
        {
            _dataLayer = new DataLayer("Data source=mydb.db");
        }
        public async Task InitDatabaseAsync(List<CD> cds)
        {
            await _dataLayer.InitAsync(cds);
        }
        public async Task<IEnumerable<CD>> GetAllCDAsync()
        {
            return await _dataLayer.GetAllAsync();
        }
    }
}
