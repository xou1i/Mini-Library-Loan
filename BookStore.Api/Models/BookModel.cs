namespace BookStore.Api.Models;

/// <summary>In-memory book model for the Books API (Id, Title, Category).</summary>
public class BookModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
}
