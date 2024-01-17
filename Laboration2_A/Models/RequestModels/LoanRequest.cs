using System.ComponentModel.DataAnnotations;

namespace Laboration2_A.Models.RequestModels;

public class LoanRequest
{
    [Required]
    public List<int> BookIds { get; set; }
    [Required]
    public int BorrowerId { get; set; }
}
