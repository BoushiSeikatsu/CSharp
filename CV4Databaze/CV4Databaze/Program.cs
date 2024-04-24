

using Dapper;
using Microsoft.Data.Sqlite;

namespace CV4Databaze
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
    public class Order
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public string Product { get; set; }
        public int Price { get; set; }
    }
    internal class Program
    {
        //Do projektu potom udělat jednu metodu co se bude dát použít na všechny tabulky pomocí reflexe to chtěl
        static void Main(string[] args)
        {
            string connectionString = "Data Source=mojedb.db";
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.SQLite);
            //Dapper ukazka
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                //Ukazka Dapper.SimpleCRUD
                int? id = connection.Insert(new Customer()
                {
                    Name = "Jan",
                    Address = "Olomouc"
                });
                Customer c = connection.Get<Customer>(id.Value);
                c.Name = c.Name.ToUpper();
                connection.Update(c);//Updatuje cely radek, takze zbytecne updatujeme data ktere jsme nezmenili
                connection.Delete(c);
                SqliteTransaction transaction = connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);//Isolation level z databazi
                int? id2 = connection.Insert(new Customer()
                {
                    Name = "Michal",
                    Address = "Ostrava"
                }, transaction);
                connection.Insert(new Order()
                {
                    CustomerId = id2,
                    Product = "Nejaky product",
                    Price = 100
                }, transaction);
                connection.Insert(new Order()
                {
                    CustomerId = id2,
                    Product = "Nejaky product2",
                    Price = 100
                }, transaction);
                transaction.Commit();
                IEnumerable<Order> orders = connection.Query<Order>("SELECT * FROM [Order]");
                foreach (var order in orders)
                {
                    Console.WriteLine("Customer Id: " +  order.CustomerId);
                }
                //Ukazka normalniho Dapperu
                connection.Execute(
                    "INSERT INTO Customer (Name, Address) VALUES (@Name, @Address)",
                    new Customer()
                    {
                        Name = "Jan",
                        Address = "Olomouc"
                    }
                    );
                long count = connection.ExecuteScalar<long>("SELECT COUNT (*) FROM Customer");
                Console.WriteLine(count);
                IEnumerable<Customer> customers = connection.Query<Customer>("SELECT * FROM Customer");
                foreach (Customer customer in customers)
                {
                    Console.WriteLine(customer.Name);
                }
                Customer oneCustomer = connection.QueryFirst<Customer>("SELECT * FROM Customer WHERE Id = @Id", new { Id = 1 });
            }
            //Microsoft.Data ukazka
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                /* using SqliteCommand cmd = connection.CreateCommand();
                cmd.CommandText = File.ReadAllText("database-create.sql");
                cmd.ExecuteNonQuery(); *///Nechci nic číst zpátky ze serveru, proto NonQuery, update apod
                using SqliteCommand insertCustCmd = connection.CreateCommand();
                insertCustCmd.CommandText = "INSERT INTO Customer (Name, Address) VALUES (@Name, @Address)";
                insertCustCmd.Parameters.AddWithValue("Name", "Tonda");
                insertCustCmd.Parameters.AddWithValue("Address", "Brno");//DBNull.Value pro Null hodnotu do databaze
                /*insertCustCmd.Parameters.Add(new SqliteParameter()
                {
                    ParameterName = "Name",
                    Value = "Address", 
                    DbType = System.Data.DbType.AnsiString//AnsiString Potrebne pro treba where podminky pro indexaci, 

                });*/
                insertCustCmd.ExecuteNonQuery();
                using SqliteCommand getCustomersCount = connection.CreateCommand();
                getCustomersCount.CommandText = "Select count(*) from Customer";
                long count = (long)getCustomersCount.ExecuteScalar();//Dostaneme 1 hodnotu
                Console.WriteLine(count);

                //Vypsani vsech customers 
                using SqliteCommand customersCmd = connection.CreateCommand();
                customersCmd.CommandText = "SELECT * From Customer";
                using SqliteDataReader reader = customersCmd.ExecuteReader();
                while(reader.Read())//Vrací true jestli tam je další hodnota
                {
                    int id = reader.GetInt32(reader.GetOrdinal("Id"));
                    string name = reader.GetString(reader.GetOrdinal("Name"));
                    string address = null;
                    if(!reader.IsDBNull(reader.GetOrdinal("Address")))
                    {
                        address = reader.GetString(reader.GetOrdinal("Address"));
                    }
                    Console.WriteLine(id + " | " + name + ": " + address);
                }

                //Insert v transakci
                using SqliteTransaction transaction = connection.BeginTransaction();
                using SqliteCommand insertIntoOrder = connection.CreateCommand();
                insertIntoOrder.Transaction = transaction;
                insertIntoOrder.CommandText = "INSERT INTO [Order] (CustomerId, Product, Price) VALUES (@CustomerId, @Product, @Price)";
                insertIntoOrder.Parameters.AddWithValue("CustomerId", 1);
                insertIntoOrder.Parameters.AddWithValue("Product", "NejakyProdukt");
                insertIntoOrder.Parameters.AddWithValue("Price", 100);
                insertIntoOrder.ExecuteNonQuery();
                transaction.Commit();

                using SqliteCommand getOrders = connection.CreateCommand();
                getOrders.CommandText = "Select * From [Order]";
                using SqliteDataReader readerOrders = getOrders.ExecuteReader();
                while(readerOrders.Read())
                {
                    /*int id = reader.GetInt32(reader.GetOrdinal("Id"));
                    int customerId = reader.GetInt32(reader.GetOrdinal("CustomerId"));
                    Console.WriteLine(id + " " + customerId);*/
                }
                //Volání procedury databáze 

                /*SqliteCommand procedureCmd = connection.CreateCommand();
                procedureCmd.CommandType = System.Data.CommandType.StoredProcedure;
                procedureCmd.CommandText = "MojeProcedura";
                procedureCmd.Parameters.Add(new SqliteParameter()
                {
                    ParameterName = "MujParametr",
                    Value = 6,
                    Direction = System.Data.ParameterDirection.Input
                });
                procedureCmd.ExecuteNonQuery();*/

                /*SqliteCommand cmd = new SqliteCommand();
                cmd.Connection = connection;*/
                //connection.Close();
            }
        }
    }
}
