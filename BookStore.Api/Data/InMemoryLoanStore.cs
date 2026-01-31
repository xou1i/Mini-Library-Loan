using BookStore.Api.Models;

namespace BookStore.Api.Data;

/// <summary>In-memory store for loans. No database.</summary>
public static class InMemoryLoanStore
{
    private static readonly List<LoanModel> Loans = new();
    private static int _nextId = 1;

    public static LoanModel? GetById(int id) => Loans.FirstOrDefault(l => l.Id == id);

    public static List<LoanModel> GetAll() => Loans.ToList();

    /// <summary>True if the book is currently on loan (no ReturnedAt).</summary>
    public static bool IsBookOnLoan(int bookId) =>
        Loans.Any(l => l.BookId == bookId && l.ReturnedAt is null);

    /// <summary>Create a loan. Returns null if book not found or already on loan.</summary>
    public static LoanModel? Create(int bookId, string borrowerName)
    {
        if (InMemoryBookStore.GetById(bookId) is null)
            return null;
        if (IsBookOnLoan(bookId))
            return null;

        var loan = new LoanModel
        {
            Id = _nextId++,
            BookId = bookId,
            BorrowerName = borrowerName.Trim(),
            LoanedAt = DateTime.UtcNow
        };
        Loans.Add(loan);
        return loan;
    }

    /// <summary>Mark loan as returned. Returns false if loan not found or already returned.</summary>
    public static bool Return(int loanId)
    {
        var loan = GetById(loanId);
        if (loan is null || loan.ReturnedAt is not null)
            return false;
        loan.ReturnedAt = DateTime.UtcNow;
        return true;
    }
}
