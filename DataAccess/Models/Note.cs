using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataAccess.Models;

public class Note
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonIgnore]
    public int Id { get; set; }
    [MaxLength(40)]
    [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Only letters and digits are allowed.")]
    public string Title { get; set; }
    [MaxLength(400)]
    public string Text { get; set; }
    public string PhotoPath { get; set; }
    [Required]
    [ForeignKey("Category")]
    public int CategoryId { get; set; }
    [JsonIgnore]
    public Category? Category { get; set; }
    [JsonIgnore]
    [ForeignKey("User")]
    public int UserId { get; set; }
    [JsonIgnore]
    public User? User { get; set; }
}