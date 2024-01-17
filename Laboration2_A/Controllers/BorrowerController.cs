using Laboration2_A.Data;
using Laboration2_A.Mappers;
using Laboration2_A.Models.DTOs;
using Laboration2_A.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Laboration2_A.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BorrowerController : ControllerBase
{
    private readonly IBorrowerMapper mapper;
    private readonly LibraryContext _context;

    public BorrowerController(IBorrowerMapper borrowerMapper, LibraryContext libraryContext)
    {
        mapper = borrowerMapper;
        _context = libraryContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BorrowerDTO>>> GetBorrowers()
    {
        var borrowers = await _context.Borrowers.AsNoTracking()
                                                .Select(b => mapper.MapBorrowerDTO(b))
                                                .ToListAsync();
        return Ok(borrowers);
    }

    [HttpPost]
    public async Task<ActionResult<BorrowerDTO>> CreateBorrower(BorrowerRequest borrowerRequest)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var newBorrower = mapper.MapBorrower(borrowerRequest);
        _context.Borrowers.Add(newBorrower);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBorrowers), new { id = newBorrower.Id }, mapper.MapBorrowerDTO(newBorrower));
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult> DeleteBorrower(int id)
    {
        var borrower = await _context.Borrowers.FindAsync(id);
        if (borrower == null) return NotFound("Borrower not found");

        _context.Borrowers.Remove(borrower);
        await _context.SaveChangesAsync();

        return Ok($"{borrower.FirstName} {borrower.LastName} with ID: {borrower.Id} was deleted");
    }
}
