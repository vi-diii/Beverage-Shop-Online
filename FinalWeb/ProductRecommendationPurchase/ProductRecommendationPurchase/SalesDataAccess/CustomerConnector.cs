using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SalesDataModel;
namespace SalesDataAccess
{
    public class CustomerConnector: Connector
    {

        public List<Customer> GetAllCustomers()
        {
            List<Customer> list = new List<Customer>();
            string sql = "select * from Customers";
            Open();

            SqlDataReader reader = Query(sql);
            while (reader.Read())
            {
                Customer cus = new Customer();
                cus.CustomerId = (uint)reader.GetInt32(0);
                cus.ContactName = reader.GetString(1);

                list.Add(cus);
            }
            reader.Close();
            Close();
            return list;
        }
        public Customer GetCustomer(uint customerId)
        {
            string sql = "select * from Customers where CustomerID = @customerId";
            SqlParameter par = new SqlParameter("@customerId", SqlDbType.Int);

            Customer cus = null;
            
            Open();

            SqlDataReader reader = Query(sql, new[] { par });
            if (reader.Read())
            {
                cus = new Customer();
                cus.CustomerId = (uint) reader.GetInt32(0);
                cus.ContactName = reader.GetString(1);
            }
            reader.Close ();
            Close();
            return cus;
        }

    }
}
