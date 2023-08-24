using l06_sql.Models;
using l06_sql.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace l06_sql.Controllers;

public class CoffeeController : Controller
{
    private readonly ILogger<CoffeeController> _logger;
    private readonly ICoffeeRepository _coffeeRepository;

    public CoffeeController(ILogger<CoffeeController> logger, ICoffeeRepository repository)
    {
        _logger = logger;
        _coffeeRepository = repository;
    }

    public IActionResult Index() {
        return RedirectToAction("List");
    }

    public IActionResult List()
    {
        return View(_coffeeRepository.GetAllCoffee());
    }

    public IActionResult Detail(int id)
    {
        var coffee = _coffeeRepository.GetCoffeeById(id);

        if (coffee == null) {
            return RedirectToAction("List");
        }

        return View(coffee);
    }

    [HttpPost]
    public IActionResult Detail(Coffee coffee) 
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        return View(_coffeeRepository.UpdateCoffee(coffee));
    }

    [HttpGet]
    public IActionResult New() 
    {
        return View();
    }

    [HttpPost]
    public IActionResult New(Coffee coffee) 
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        
        _coffeeRepository.CreateCoffee(coffee);

        return RedirectToAction("List");
    }

    public IActionResult Delete(int id) 
    {
        _coffeeRepository.DeleteCoffeeById(id);

        return RedirectToAction("List");
    }
}
