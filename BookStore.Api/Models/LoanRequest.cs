namespace BookStore.Api.Models;

/// <summary>Request body for creating a loan (book id and borrower name).</summary>
public class LoanRequest
{
    public int BookId { get; set; }
    public string BorrowerName { get; set; } = string.Empty;
}
