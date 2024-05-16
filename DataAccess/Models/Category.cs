using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataAccess.Models;

public class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonIgnore]
    public int Id { get; set; }
    [MaxLength(40)]
    [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Only letters and digits are allowed.")]
    public string Name { get; set; }
    public List<Note> Notes { get; set; } = new();
    [JsonIgnore]
    [ForeignKey("User")]
    public int UserId { get; set; }
    [JsonIgnore]
    public User? User { get; set; }
}