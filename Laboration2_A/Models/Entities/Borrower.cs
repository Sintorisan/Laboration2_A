using System.ComponentModel.DataAnnotations;

namespace Laboration2_A.Models.Entities;

public class Borrower
{
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string SSN { get; set; }
    public ICollection<Loan> Loans { get; set; }

}
