using Laboration2_A.Data;
using Laboration2_A.Mappers;
using Laboration2_A.Models.DTOs;
using Laboration2_A.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Laboration2_A.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoanController : ControllerBase
{
    private readonly ILoanMapper mapper;
    private readonly LibraryContext _context;

    public LoanController(ILoanMapper loanMapper, LibraryContext libraryContext)
    {
        mapper = loanMapper;
        _context = libraryContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LoanDTO>>> GetLoans()
    {
        var loans = await _context.Loans
                                  .Include(l => l.Books)
                                  .Include(l => l.Borrower)
                                  .ToListAsync();

        var loanDTOs = loans.Select(loan => mapper.MapLoanDTO(loan)).ToList();

        return Ok(loanDTOs);
    }

    [HttpPost]
    public async Task<ActionResult> CreateLoan(LoanRequest loanRequest)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            //Changes the IDs from the loanRequest, to actual obj.
            var borrower = await _context.Borrowers.FindAsync(loanRequest.BorrowerId);
            var books = await _context.Books.Where(b => loanRequest.BookIds.Contains(b.Id)).ToListAsync();

            if (!books.Any() || borrower == null)
            {
                await transaction.RollbackAsync();
                return BadRequest("No valid IDs provided for books or borrower");
            }

            foreach (var book in books)
            {
                if (!book.IsAvailable)
                {
                    await transaction.RollbackAsync();
                    return BadRequest($"Book with ID {book.Id} is already on a loan");
                }
                book.IsAvailable = false;
            }

            var loan = mapper.MapLoan(loanRequest, books, borrower);
            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return CreatedAtAction(nameof(GetLoans), new { id = loan.Id }, mapper.MapLoanDTO(loan));
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return StatusCode(500, $"An error occurred while creating the loan: {ex.Message}");
        }
    }

    [HttpPut("end/{id}")]
    public async Task<ActionResult<LoanDTO>> EndLoan(int id)
    {
        var loan = await _context.Loans.Include(l => l.Books)
                                       .Include(l => l.Borrower)
                                       .FirstOrDefaultAsync(l => l.Id == id);

        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            if (loan == null)
            {
                await transaction.RollbackAsync();
                return NotFound($"Loan with ID {id} was not found");
            }
            if (loan.IsLoanOpen == false)
            {
                await transaction.RollbackAsync();
                return BadRequest($"Loan with ID {id} is already closed");
            }

            foreach (var book in loan.Books)
            {
                book.IsAvailable = true;
            }
            loan.ReturnDate = DateTime.Now;
            loan.IsLoanOpen = false;

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            return Ok(mapper.MapLoanDTO(loan));
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return StatusCode(500, $"An error occurred while ending the loan: {ex.Message}");
        }
    }
}
