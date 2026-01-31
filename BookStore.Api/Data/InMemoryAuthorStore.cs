using BookStore.Api.Models;

namespace BookStore.Api.Data;

/// <summary>In-memory store for authors. No database.</summary>
public static class InMemoryAuthorStore
{
    private static readonly List<AuthorModel> Authors = new()
    {
        new AuthorModel { Id = 1, Name = "George Orwell" },
        new AuthorModel { Id = 2, Name = "Yuval Noah Harari" }
    };

    private static int _nextId = 3;

    public static List<AuthorModel> GetAll() => Authors.ToList();

    public static AuthorModel? GetById(int id) => Authors.FirstOrDefault(a => a.Id == id);

    public static AuthorModel Add(string name)
    {
        var author = new AuthorModel { Id = _nextId++, Name = name.Trim() };
        Authors.Add(author);
        return author;
    }

    public static bool Delete(int id)
    {
        var author = GetById(id);
        if (author is null) return false;
        Authors.Remove(author);
        return true;
    }
}
