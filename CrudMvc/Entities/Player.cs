using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CrudMvc.Entities;
public class Player
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Position { get; set; }
    public int TeamId { get; set; }
    [Required]
    public Team? Team { get; set; }
}