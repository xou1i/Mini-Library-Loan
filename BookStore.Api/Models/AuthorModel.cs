namespace BookStore.Api.Models;

/// <summary>Author model for the Authors API (in-memory).</summary>
public class AuthorModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
