using Laboration2_A.Models.DTOs;
using Laboration2_A.Models.Entities;
using Laboration2_A.Models.RequestModels;

namespace Laboration2_A.Mappers;

public interface IBookMapper
{
    public BookDTO MapBookDTO(Book book);

    public Book MapBook(BookRequest book);

}

public class BookMapper : IBookMapper
{
    public BookDTO MapBookDTO(Book book)
    {
        return new BookDTO
        {
            Id = book.Id,
            Title = book.Title,
            ISBN = book.ISBN,
            ReleaseYear = book.ReleaseYear,
            IsAvailable = book.IsAvailable
        };
    }

    public Book MapBook(BookRequest bookRequest)
    {
        return new Book
        {
            Title = bookRequest.Title,
            ISBN = bookRequest.ISBN,
            ReleaseYear = bookRequest.ReleaseYear
        };
    }
}
