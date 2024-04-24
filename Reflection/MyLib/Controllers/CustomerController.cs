using MyLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib.Controllers
{
    class CustomerController
    {

        private static List<Customer> customers = new List<Customer>()
        {
            new Customer(){ Id = 1, Age = 34, IsActive = true, Name = "Jan" },
            new Customer(){ Id = 2, Age = 46, IsActive = false, Name = "Tom" },
            new Customer(){ Id = 3, Age = 36, IsActive = true, Name = "Michala" }
        };

        /*public string List()
        {
            StringBuilder result = new StringBuilder();
            foreach(var customer in customers)
            {
                result.AppendLine(customer.Name);
            }
            return result.ToString();

        }*/
        public string Add(string Name, int Age, bool IsActive)
        {
            int newId = customers.Select(x => x.Id).DefaultIfEmpty(0).Max() + 1;
            customers.Add(new Customer() { Id = newId, Name = Name, Age = Age, IsActive = IsActive });
            return newId + "";
        }
        public string List(int limit)
        {
            StringBuilder result = new StringBuilder();
            if (limit > customers.Count)
            {
                limit = customers.Count;
            }
            for (int i = 0; i<limit;i++)
            {
                result.AppendLine(customers[i].Name);
            }
            return result.ToString();
        }

    }
}
