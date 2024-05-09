using ASPNETPractice.Models;
using Dapper;
using Microsoft.Data.Sqlite;
using System.Globalization;

namespace ASPNETPractice.Database
{
    public class DataLayer
    {
        private readonly string _connectionString;

        public DataLayer(string connectionString)
        {
            _connectionString  = connectionString;
        }

        public async Task InitAsync(List<CD> cds)
        {
            using(var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                string createTable = @"CREATE TABLE IF NOT EXISTS CD(" +
                    "ID INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "Title Varchar(50) NOT NULL," +
                    "Artist VARCHAR(50) NOT NULL," +
                    "Country VARCHAR(50) NOT NULL," +
                    "Company VARCHAR(50) NOT NULL," +
                    "Price FLOAT NOT NULL," +
                    "Year INTEGER NOT NULL"+
                    ")";
                await connection.ExecuteAsync(createTable);
                int count = await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM CD");
                if(count == 0)
                {
                    foreach (CD cd in cds)
                    {
                        await InsertAsync(cd);
                    }
                }
            }
        }
        public async Task InsertAsync(CD cd)
        {
            using(var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync("INSERT INTO CD(TITLE,ARTIST,COUNTRY,COMPANY,PRICE,YEAR)" +
                    " VALUES " +
                    "(@Title,@Artist,@Country,@Company,@Price,@Year)",cd);
            }
        }

        public async Task<IEnumerable<CD>> GetAllAsync()
        {
            using(var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                IEnumerable<CD> cds = await connection.QueryAsync<CD>("SELECT * FROM CD");
                return cds;
            }
        }
    }
}
