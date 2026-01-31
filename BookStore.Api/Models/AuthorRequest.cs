namespace BookStore.Api.Models;

/// <summary>Request body for creating an author (Name only).</summary>
public class AuthorRequest
{
    public string Name { get; set; } = string.Empty;
}
