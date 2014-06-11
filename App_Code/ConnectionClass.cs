using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using Entities;

public static class ConnectionClass
{
    private static SqlConnection conn;
    private static SqlCommand command;

    static ConnectionClass()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["g3f4.CoffeeDB.dbo"].ToString();
        conn = new SqlConnection(connectionString);
        command = new SqlCommand("", conn);
    }

    #region Coffee
    public static Coffee GetCoffeeByID(int id)
    {
        string query = string.Format("SELECT * FROM coffee WHERE id =  '{0}'", id);
        Coffee coffee = null;

        try
        {
            conn.Open();
            command.CommandText = query;
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string name = reader.GetString(1);
                string type = reader.GetString(2);
                double price = reader.GetDouble(3);
                string roast = reader.GetString(4);
                string country = reader.GetString(5);
                string image = reader.GetString(6);
                string review = reader.GetString(7);

                coffee = new Coffee(id, name, type, price, roast, country, image, review);
            }
        }
        finally
        {
            conn.Close();
        }

        return coffee;
    }

    public static ArrayList GetCoffeeByType(string coffeeType)
    {
        ArrayList list = new ArrayList();
        string query = string.Format("SELECT * FROM coffee WHERE type LIKE '{0}'", coffeeType);

        try
        {
            conn.Open();
            command.CommandText = query;
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                string type = reader.GetString(2);
                double price = reader.GetDouble(3);
                string roast = reader.GetString(4);
                string country = reader.GetString(5);
                string image = reader.GetString(6);
                string review = reader.GetString(7);

                Coffee coffee = new Coffee(id, name, type, price, roast, country, image, review);
                list.Add(coffee);
            }
        }
        finally
        {
            conn.Close();
        }

        return list;
    }

    public static void AddCoffee(Coffee coffee)
    {
        string query = string.Format(
            @"INSERT INTO coffee VALUES ('{0}', '{1}', @prices, '{2}', '{3}','{4}', '{5}')",
            coffee.Name, coffee.Type, coffee.Roast, coffee.Country, coffee.Image, coffee.Review);
        command.CommandText = query;
        command.Parameters.Add(new SqlParameter("@prices", coffee.Price));
        try
        {
            conn.Open();
            command.ExecuteNonQuery();
        }
        finally
        {
            conn.Close();
            command.Parameters.Clear();
        }
    }
    #endregion

    #region Users
    public static User LoginUser(string name, string password)
    {
        //Check if user exists
        string query = string.Format("SELECT COUNT(*) FROM CoffeeDB.dbo.users WHERE name = '{0}'", name);
        command.CommandText = query;

        try
        {
            conn.Open();
            int amountOfUsers = (int)command.ExecuteScalar();

            if (amountOfUsers == 1)
            {
                //User exists, check if the passwords match
                query = string.Format("SELECT password FROM users WHERE name = '{0}'", name);
                command.CommandText = query;
                string dbPassword = command.ExecuteScalar().ToString();

                if (dbPassword == password)
                {
                    //Passwords match. Login and password data are known to us.
                    //Retrieve further user data from the database
                    query = string.Format("SELECT email, user_type FROM users WHERE name = '{0}'", name);
                    command.CommandText = query;

                    SqlDataReader reader = command.ExecuteReader();
                    User user = null;

                    while (reader.Read())
                    {
                        string email = reader.GetString(0);
                        string type = reader.GetString(1);

                        user = new User(name, password, email, type);
                    }
                    return user;
                }
                else
                {
                    //Passwords do not match
                    return null;
                }
            }
            else
            {
                //User does not exist
                return null;
            }
        }
        finally
        {

            conn.Close();
        }
    }

    public static string RegisterUser(User user)
    {
        //Check if user exists
        string query = string.Format("SELECT COUNT(*) FROM users WHERE name = '{0}'", user.Name);
        command.CommandText = query;

        try
        {
            conn.Open();
            int amountOfUsers = (int)command.ExecuteScalar();

            if (amountOfUsers < 1)
            {
                //User does not exist, create a new user
                query = string.Format("INSERT INTO users VALUES ('{0}', '{1}', '{2}', '{3}')", user.Name, user.Password,
                                      user.Email, user.Type);
                command.CommandText = query;
                command.ExecuteNonQuery();
                return "User registered!";
            }
            else
            {
                //User exists
                return "A user with this name already exists";
            }
        }
        finally
        {
            conn.Close();
        }
    }
    #endregion

    #region Orders
    public static void AddOrder(ArrayList orders)
    {
        try
        {
            command.CommandText = @"INSERT INTO orders VALUES (@client, @productName, @amount, @price, @date, @orderSent)";
            conn.Open();

            foreach (Order order in orders)
            {
                command.Parameters.Add(new SqlParameter("@client", order.Client));
                command.Parameters.Add(new SqlParameter("@productName", order.ProductName));
                command.Parameters.Add(new SqlParameter("@amount", order.Amount));
                command.Parameters.Add(new SqlParameter("@price", order.Price));
                command.Parameters.Add(new SqlParameter("@date", order.Date));
                command.Parameters.Add(new SqlParameter("@orderSent", order.OrderSent));
                command.ExecuteNonQuery();
                command.Parameters.Clear();
            }
        }
        finally
        {
            conn.Close();
        }
    }

    #endregion
}