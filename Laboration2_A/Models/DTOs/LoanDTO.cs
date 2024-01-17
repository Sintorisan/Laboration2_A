namespace Laboration2_A.Models.DTOs;

public class LoanDTO
{
    public int Id { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public bool IsLoanOpen { get; set; }
    public List<int> BookIds { get; set; }
    public int BorrowerId { get; set; }
}
