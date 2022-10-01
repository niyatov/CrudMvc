using System.ComponentModel.DataAnnotations;
namespace CrudMvc.Models;

public class Player
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    public string? Position { get; set; }
    public int TeamId { get; set; }
    public string? Team { get; set; }
    public int Count { get; set; }
    
    
}