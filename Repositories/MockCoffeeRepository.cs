using l06_sql.Models;

namespace l06_sql.Repositories;

public class MockCoffeeRepository : ICoffeeRepository
{     
    private List<Coffee> coffeeList;

    public MockCoffeeRepository() {
        coffeeList = new List<Coffee>();

        coffeeList.Add(new Coffee {
            CoffeeId = 1,
            Name = "Espresso",
            Description = "Fresh roasted & brewed shot of espresso",
            Price = 2.99
        });
        coffeeList.Add(new Coffee {
            CoffeeId = 2,
            Name = "Coffee",
            Description = "House brewed blend of coffee",
            Price = 1.99
        });
        coffeeList.Add(new Coffee {
            CoffeeId = 3,
            Name = "Latte",
            Description = "Organically sourced steamed milk with a shot of our fine espresso",
            Price = 4.99
        });
    }

    public Coffee CreateCoffee(Coffee newCoffee)
    {
        // Find current max CoffeeId
        var maxId = coffeeList.Select(c => c.CoffeeId)
            .DefaultIfEmpty()
            .Max();

        // Increment CoffeeId for new coffee
        newCoffee.CoffeeId = maxId + 1;

        coffeeList.Add(newCoffee);
        return newCoffee;
    }

    public void DeleteCoffeeById(int coffeeId)
    {
        var coffeeToRemove = coffeeList.Find(c => c.CoffeeId == coffeeId);
        if (coffeeToRemove != null) {
            coffeeList.Remove(coffeeToRemove);
        }
    }

    public IEnumerable<Coffee> GetAllCoffee()
    {
        return coffeeList;
    }

    public Coffee GetCoffeeById(int coffeeId)
    {
        return coffeeList.Find(c => c.CoffeeId == coffeeId);
    }

    public Coffee UpdateCoffee(Coffee newCoffee)
    {
        var existingCoffee = coffeeList.Find(c => c.CoffeeId == newCoffee.CoffeeId);

        // If coffee exists, update properties
        if (existingCoffee != null) {
            existingCoffee.Name = newCoffee.Name;
            existingCoffee.Description = newCoffee.Description;
            existingCoffee.Price = newCoffee.Price;
        }
        
        return existingCoffee;
    }
}