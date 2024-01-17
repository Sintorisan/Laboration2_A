using Laboration2_A.Data;
using Laboration2_A.Mappers;
using Laboration2_A.Models.DTOs;
using Laboration2_A.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Laboration2_A.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IBookMapper mapper;
    private readonly LibraryContext _context;

    public BookController(IBookMapper bookMapper, LibraryContext libraryContext)
    {
        mapper = bookMapper;
        _context = libraryContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks()
    {
        var books = await _context.Books.AsNoTracking()
                                        .Select(b => mapper.MapBookDTO(b))
                                        .ToListAsync();
        return Ok(books);
    }

    [HttpGet("get/{id}")]
    public async Task<ActionResult<BookDTO>> GetSingleBook(int id)
    {
        var book = await _context.Books.FindAsync(id);

        if (book == null) return NotFound($"Book with ID {id} not found");

        return Ok(mapper.MapBookDTO(book));
    }

    [HttpPost]
    public async Task<ActionResult<BookDTO>> CreateBook(BookRequest bookRequest)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var newBook = mapper.MapBook(bookRequest);
        _context.Books.Add(newBook);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSingleBook), new { id = newBook.Id }, mapper.MapBookDTO(newBook));
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult> DeleteBook(int id)
    {
        var book = await _context.Books.FindAsync(id);

        if (book == null) return NotFound("Book not found");

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return Ok($"{book.Title} was removed");
    }
}
