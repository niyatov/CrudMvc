using System.ComponentModel.DataAnnotations;
namespace CrudMvc.Models;

public class CreatePlayer
{
    [Required]
    public string? Name { get; set; }
    public string? Position { get; set; }
}