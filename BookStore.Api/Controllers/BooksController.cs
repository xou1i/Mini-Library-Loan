using BookStore.Api.Common;
using BookStore.Api.Data;
using BookStore.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

/// <summary>Books API: in-memory store, no database. Id, Title, Category.</summary>
public class BooksController : BaseController
{
    /// <summary>Get all books.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<BookModel>), StatusCodes.Status200OK)]
    public ActionResult<List<BookModel>> GetBooks()
    {
        var result = InMemoryBookStore.GetAll();
        return Ok(result);
    }

    /// <summary>Get a single book by id. Returns 404 if not found.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(BookModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<BookModel> GetBookById(int id)
    {
        var result = InMemoryBookStore.GetById(id);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    /// <summary>Get all books in the given category (case-insensitive). Returns empty list if none.</summary>
    [HttpGet("category/{category}")]
    [ProducesResponseType(typeof(List<BookModel>), StatusCodes.Status200OK)]
    public ActionResult<List<BookModel>> GetBooksByCategory(string category)
    {
        var result = InMemoryBookStore.GetByCategory(category);
        return Ok(result);
    }

    /// <summary>Create a book. Returns 400 if title is duplicate (case-insensitive) or category is invalid.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(BookModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<BookModel> CreateBook([FromBody] BookRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
            return BadRequest("Title is required.");
        if (InMemoryBookStore.ExistsByTitle(request.Title))
            return BadRequest("A book with the same title already exists.");
        if (!InMemoryBookStore.IsValidCategory(request.Category))
            return BadRequest($"Invalid category. Allowed: {string.Join(", ", InMemoryBookStore.AllowedCategories)}");

        var book = InMemoryBookStore.Add(request.Title, request.Category);
        return Ok(book);
    }

    /// <summary>Update a book's Title and Category by id. Returns 404 if not found, 400 if category invalid.</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult UpdateBook(int id, [FromBody] BookRequest request)
    {
        if (InMemoryBookStore.GetById(id) is null)
            return NotFound();
        if (!InMemoryBookStore.IsValidCategory(request.Category))
            return BadRequest($"Invalid category. Allowed: {string.Join(", ", InMemoryBookStore.AllowedCategories)}");

        InMemoryBookStore.Update(id, request.Title, request.Category);
        return NoContent();
    }

    /// <summary>Delete a book by id.</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult DeleteBook(int id)
    {
        if (!InMemoryBookStore.Delete(id))
            return NotFound();
        return NoContent();
    }
}
