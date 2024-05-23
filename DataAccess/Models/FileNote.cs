using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models;

public class FileNote
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    
    [ForeignKey("Note")]
    public int NoteId { get; set; }
    public Note Note { get; set; }
    
    [ForeignKey("User")]
    public int UserId { get; set; }
    public User? User { get; set; }
}