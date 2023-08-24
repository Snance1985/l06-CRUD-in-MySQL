using System.ComponentModel.DataAnnotations;

namespace l06_sql.Models;

public class Coffee {
    public int CoffeeId { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }
    
    [Required, Range(0, 20, ErrorMessage = "You must enter a price between 0 and 20.")]
    public double Price { get; set; }
}