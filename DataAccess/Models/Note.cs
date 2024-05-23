using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataAccess.Models;

public class Note
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [MaxLength(40)]
    [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Only letters and digits are allowed.")]
    public string Title { get; set; }
    
    [MaxLength(400)]
    public string Text { get; set; }
    
    [Required]
    [ForeignKey("Category")]
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    
    [ForeignKey("User")]
    public int UserId { get; set; }
    public User? User { get; set; }
    
    public List<FileNote> FileNotes { get; set; }
}