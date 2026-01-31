namespace BookStore.Api.Models;

/// <summary>Loan record: who borrowed which book and when.</summary>
public class LoanModel
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public string BorrowerName { get; set; } = string.Empty;
    public DateTime LoanedAt { get; set; }
    public DateTime? ReturnedAt { get; set; }
}
