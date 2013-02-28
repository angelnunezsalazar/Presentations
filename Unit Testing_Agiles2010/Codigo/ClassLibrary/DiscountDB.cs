using System;
using System.Configuration;
using System.Data.SqlClient;

namespace ClassLibrary
{
    public interface IDiscountDB
    {
        int GetDiscount(CustomerCategory customerCategory);
        void Save(Order order);
        Order GetOrderById(int id);
    }

    public class DiscountDB : IDiscountDB
    {
        public int GetDiscount(CustomerCategory customerCategory)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                var command = new SqlCommand("select Percentage from Discount where CustomerCategory=@CustomerCategory", conn);

                command.Parameters.AddWithValue("CustomerCategory", customerCategory);

                conn.Open();

                object obj = command.ExecuteScalar();

                if (obj != null)
                {
                    int valor = (int) obj;
                    return valor;
                }
                throw new Exception("Value not found");
            }
        }

        public void Save(Order order)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                using (var command = new SqlCommand("Insert into Orders values(@Id,@Total)", conn))
                {

                    command.Parameters.AddWithValue("Id", order.Id);
                    command.Parameters.AddWithValue("Total", order.Total);

                    conn.Open();
                    int rows = command.ExecuteNonQuery();
                    if (rows == 0)
                    {
                        throw new Exception("Operation Failed");
                    }
                }
            }
        }

        public Order GetOrderById(int id)
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                using (var command = new SqlCommand("select Id,Total from Orders where Id=@Id", conn))
                {

                    command.Parameters.AddWithValue("Id", id);

                    conn.Open();

                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        return new Order {Id = reader.GetInt32(0), Total = reader.GetInt32(1)};
                    }
                    return null;
                }
            }
        }

    }
}