using Laboration2_A.Models.DTOs;
using Laboration2_A.Models.Entities;
using Laboration2_A.Models.RequestModels;

namespace Laboration2_A.Mappers;

public interface IBorrowerMapper
{
    public BorrowerDTO MapBorrowerDTO(Borrower borrower);

    public Borrower MapBorrower(BorrowerRequest borrower);
}

public class BorrowerMapper : IBorrowerMapper
{
    public BorrowerDTO MapBorrowerDTO(Borrower borrower)
    {
        return new BorrowerDTO
        {
            Id = borrower.Id,
            FirstName = borrower.FirstName,
            LastName = borrower.LastName
        };
    }

    public Borrower MapBorrower(BorrowerRequest borrowerRequest)
    {
        return new Borrower
        {
            FirstName = borrowerRequest.FirstName,
            LastName = borrowerRequest.LastName,
            SSN = borrowerRequest.SSN
        };
    }
}

