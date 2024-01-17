using System.ComponentModel.DataAnnotations;

namespace Laboration2_A.Models.Entities;

public class Book
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string ISBN { get; set; }
    [Required]
    public int ReleaseYear { get; set; }
    public bool IsAvailable { get; set; } = true;
    public ICollection<Loan> Loans { get; set; }

}
