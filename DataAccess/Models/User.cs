using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataAccess.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonIgnore]
    public int Id { get; set; }
    [Required]
    public string UserName { get; set; }
    [JsonIgnore]
    public byte[] PasswordHash  { get; set; }
    [JsonIgnore]
    public byte[] PasswordSalt { get; set; }
}