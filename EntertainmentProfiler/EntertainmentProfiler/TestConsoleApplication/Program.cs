using EPShared.DBContext;
using EPShared.Shared_Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomerInfo obj = new CustomerInfo();

            var x = obj.FindUserByUserName("johnsmith123");

            //obj.CreateNewUser("Smith", "John", 21, "john.smith3@gmail.com", "1234567890", "M", "johnsmith123", "password123");
            Console.ReadKey();

        }
    }
}
