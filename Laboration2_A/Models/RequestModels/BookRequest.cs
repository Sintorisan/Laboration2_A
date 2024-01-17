using System.ComponentModel.DataAnnotations;

namespace Laboration2_A.Models.RequestModels;

public class BookRequest
{
    [Required]
    public string Title { get; set; }
    [Required]
    public string ISBN { get; set; }
    [Required]
    public int ReleaseYear { get; set; }
}