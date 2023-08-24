using l06_sql.Models;
using MySql.Data.MySqlClient;

namespace l06_sql.Repositories;

public class SqlCoffeeRepository : ICoffeeRepository
{
    private static string _myConnectionString = "server=127.0.0.1;uid=root;pwd=W0rthy1sth3L4mb!;database=mydb";

    public Coffee CreateCoffee(Coffee newCoffee)
    {
        var conn = new MySql.Data.MySqlClient.MySqlConnection(_myConnectionString);
        conn.Open();

        string query = "INSERT INTO coffee (name, description, price) VALUES (@name, @description, @price);";
        var command = new MySqlCommand(query, conn);

        command.Parameters.AddWithValue("@name", newCoffee.Name);
        command.Parameters.AddWithValue("@description", newCoffee.Description);
        command.Parameters.AddWithValue("@price", newCoffee.Price);
        command.Prepare();

        command.ExecuteNonQuery();
        int id = (int)command.LastInsertedId;
        conn.Close();

        if (id > 0)
        {
            newCoffee.CoffeeId = id;
            return newCoffee;
        }

        return null;
    }


public void DeleteCoffeeById(int coffeeId)
{
    var conn = new MySql.Data.MySqlClient.MySqlConnection(_myConnectionString);
    conn.Open();

    string query = "DELETE FROM coffee WHERE coffeeId = @id;";
    var command = new MySqlCommand(query, conn);

    command.Parameters.AddWithValue("@id", coffeeId);
    command.Prepare();

    command.ExecuteNonQuery();

    conn.Close();
}


    public IEnumerable<Coffee> GetAllCoffee()
    {
        var coffeeList = new List<Coffee>();

        var conn = new MySql.Data.MySqlClient.MySqlConnection(_myConnectionString);
        conn.Open();

        string query = "SELECT * FROM coffee;";
        var command = new MySqlCommand(query, conn);
        var reader = command.ExecuteReader();

        while (reader.Read())
        {
            coffeeList.Add(new Coffee
            {
                CoffeeId = reader.GetInt32("coffeeId"),
                Name = reader.GetString("name"),
                Description = reader.GetString("description"),
                Price = reader.GetDouble("price")
            });
        }

        reader.Close();
        conn.Close();

        return coffeeList;
    }


    public Coffee GetCoffeeById(int coffeeId)
    {
        Coffee coffee = null;

        var conn = new MySql.Data.MySqlClient.MySqlConnection(_myConnectionString);
        conn.Open();

        string query = "SELECT * FROM coffee WHERE coffeeId = @id;";
        var command = new MySqlCommand(query, conn);

        command.Parameters.AddWithValue("@id", coffeeId);
        command.Prepare();

        var reader = command.ExecuteReader();

        reader.Read();

        if (reader.HasRows)
        {
            coffee = new Coffee
            {
                CoffeeId = reader.GetInt32("coffeeId"),
                Name = reader.GetString("name"),
                Description = reader.GetString("description"),
                Price = reader.GetDouble("price")
            };
        }

        reader.Close();
        conn.Close();

        return coffee;
    }


    public Coffee UpdateCoffee(Coffee newCoffee)
    {
        var conn = new MySql.Data.MySqlClient.MySqlConnection(_myConnectionString);
        conn.Open();

        string query = "UPDATE coffee SET name = @name, description = @description, price = @price " +
            "WHERE coffeeId = @id";
        var command = new MySqlCommand(query, conn);

        command.Parameters.AddWithValue("@name", newCoffee.Name);
        command.Parameters.AddWithValue("@description", newCoffee.Description);
        command.Parameters.AddWithValue("@price", newCoffee.Price);
        command.Parameters.AddWithValue("@id", newCoffee.CoffeeId);
        command.Prepare();

        command.ExecuteNonQuery();

        conn.Close();

        return newCoffee;
    }

}
