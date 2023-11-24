using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIPruebaData.Repository;
using WebAPIPruebaModels;

namespace WebAPIPruebaData
{
    public class Customer_Repository : ICustomerRepository
    {
        private readonly MySQLConfiguration _connectionString;
        public Customer_Repository(MySQLConfiguration connectionString) 
        {
            _connectionString = connectionString;
        }
        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            var db = dbConnection();

            var sql = @"SELECT customer_id, store_id, first_name, last_name, email, address_id, active 
                        FROM customer";

            return await db.QueryAsync<Customer>(sql, new { });
        }

        public async Task<Customer> GetCustomerDetails(int id)
        {
            var db = dbConnection();

            var sql = @"SELECT customer_id, store_id, first_name, last_name, email, address_id, active 
                        FROM customer 
                        WHERE customer_id = " + id.ToString();

            return await db.QueryFirstOrDefaultAsync<Customer>(sql);
        }

        public async Task<bool> InsertCustomer(Customer customer)
        {
            var db = dbConnection();

            //Hay que revisar la consulta porque no reconoce la columna del primer nombre 
            var sql = $@"INSERT INTO customer ( store_id, first_name, last_name, email, address_id, active) 
                        VALUES ({customer.store_id}, {customer.firt_name}, {customer.last_name}, {customer.email}, {customer.address_id}, {customer.active})";

            var result = await db.ExecuteAsync(sql);

            return result > 0;
        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            var db = dbConnection();

            var sql = $@"UPDATE customer 
                       SET store_id = { customer.store_id}, 
                            first_name = '{customer.firt_name}', 
                            last_name = '{customer.last_name}', 
                            email = '{customer.email}', 
                            address_id = {customer.address_id}, 
                            active = {customer.active}
                       WHERE customer_id = {customer.customer_id}";

            var result = await db.ExecuteAsync(sql);

            return result > 0;
        }
        public async Task<bool> DeleteCustomer(int id)
        {
            var db = dbConnection();

            var sql = @"DELETE FROM customer WHERE customer_id = @Id";

            var result = await db.ExecuteAsync(sql, new { customer_id =  id });

            return result > 0;
        }

    }
}
