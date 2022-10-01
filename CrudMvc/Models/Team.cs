using System.ComponentModel.DataAnnotations;
namespace CrudMvc.Models;

public class Team
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    public int CountPlayer { get; set; }
    
}