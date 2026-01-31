using BookStore.Api.Models;

namespace BookStore.Api.Data;

/// <summary>In-memory store for books with Title and Category. No database.</summary>
public static class InMemoryBookStore
{
    public static readonly string[] AllowedCategories = { "Novel", "Science", "History" };

    private static readonly List<BookModel> Books = new()
    {
        new BookModel { Id = 1, Title = "1984", Category = "Novel" },
        new BookModel { Id = 2, Title = "A Brief History of Time", Category = "Science" },
        new BookModel { Id = 3, Title = "Sapiens", Category = "History" }
    };

    private static int _nextId = 4;

    public static BookModel? GetById(int id) => Books.FirstOrDefault(b => b.Id == id);

    public static List<BookModel> GetAll() => Books.ToList();

    public static List<BookModel> GetByCategory(string category)
    {
        return Books
            .Where(b => string.Equals(b.Category, category, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public static bool ExistsByTitle(string title)
    {
        return Books.Any(b => string.Equals(b.Title, title, StringComparison.OrdinalIgnoreCase));
    }

    public static bool IsValidCategory(string category)
    {
        return AllowedCategories.Contains(category, StringComparer.OrdinalIgnoreCase);
    }

    public static BookModel Add(string title, string category)
    {
        var book = new BookModel { Id = _nextId++, Title = title.Trim(), Category = category };
        Books.Add(book);
        return book;
    }

    public static bool Update(int id, string title, string category)
    {
        var book = GetById(id);
        if (book is null) return false;
        book.Title = title.Trim();
        book.Category = category;
        return true;
    }

    public static bool Delete(int id)
    {
        var book = GetById(id);
        if (book is null) return false;
        Books.Remove(book);
        return true;
    }
}
