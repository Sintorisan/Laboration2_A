using System.ComponentModel.DataAnnotations;

namespace Laboration2_A.Models.Entities;

public class Loan
{
    public int Id { get; set; }
    [Required]
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public bool IsLoanOpen { get; set; } = true;
    [Required]
    public ICollection<Book> Books { get; set; }
    [Required]
    public int BorrowerId { get; set; }
    public Borrower? Borrower { get; set; }
}