namespace BookStore.Api.Models;

/// <summary>Request body for creating or updating a book (Title and Category).</summary>
public class BookRequest
{
    public string Title { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
}
