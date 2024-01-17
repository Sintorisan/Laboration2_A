using Laboration2_A.Models.DTOs;
using Laboration2_A.Models.Entities;
using Laboration2_A.Models.RequestModels;

namespace Laboration2_A.Mappers;

public interface ILoanMapper
{
    public LoanDTO MapLoanDTO(Loan loan);

    public Loan MapLoan(LoanRequest loanRequest, IEnumerable<Book> books, Borrower borrower);

}
public class LoanMapper : ILoanMapper
{
    public LoanDTO MapLoanDTO(Loan loan)
    {
        return new LoanDTO
        {
            Id = loan.Id,
            LoanDate = loan.LoanDate,
            ReturnDate = loan.ReturnDate,
            IsLoanOpen = loan.IsLoanOpen,
            BookIds = loan.Books.Select(b => b.Id).ToList(),
            BorrowerId = loan.BorrowerId
        };
    }

    public Loan MapLoan(LoanRequest loanRequest, IEnumerable<Book> books, Borrower borrower)
    {
        return new Loan
        {
            LoanDate = DateTime.Now,
            ReturnDate = null,
            Books = new List<Book>(books),
            Borrower = borrower,
            BorrowerId = borrower.Id
        };
    }
}