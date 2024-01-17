namespace Laboration2_A.Models.DTOs;

public class BookDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ISBN { get; set; }
    public int ReleaseYear { get; set; }
    public bool IsAvailable { get; set; }
}
