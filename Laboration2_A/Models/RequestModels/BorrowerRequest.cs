using System.ComponentModel.DataAnnotations;

namespace Laboration2_A.Models.RequestModels;

public class BorrowerRequest
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string SSN { get; set; }
}
