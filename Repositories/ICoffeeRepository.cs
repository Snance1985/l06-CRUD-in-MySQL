using l06_sql.Models;

namespace l06_sql.Repositories;

public interface ICoffeeRepository
{
    IEnumerable<Coffee> GetAllCoffee();
    Coffee GetCoffeeById(int coffeeId);
    Coffee CreateCoffee(Coffee newCoffee);
    Coffee UpdateCoffee(Coffee newCoffee);
    void DeleteCoffeeById(int coffeeId);
}