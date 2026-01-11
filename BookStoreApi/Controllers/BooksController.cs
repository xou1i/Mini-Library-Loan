using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers;

[ApiController]
[Route("/[controller]")]
public class BooksController : ControllerBase
{

    private readonly List<string> allowedCategories = new List<string>()
    {
        "Novel",
        "Documentary"
    };
    public static List<Book> Books = new()
    {
        new Book() { Id = 1, Title = "Xyz", Category = "Novel" },
        new Book() { Id = 2, Title = "Abc", Category = "Documentary" },
    };


    [HttpGet]
    public ActionResult<List<Book>> Get()
    {
        return Ok(Books);
    }

    [HttpPost]
    public ActionResult CreateBook([FromBody] BookRequest request)
    {
        var newId = Books.Max(b => b.Id) + 1;
        if (!allowedCategories.Contains(request.Category))
            return BadRequest("Category is invalid");
        var newBook = new Book()
        {
            Title = request.Title,
            Category = request.Category,
            Id = newId
        };
        Books.Add(newBook);
        return Created();
    }

    [HttpGet("{id:int}")]
    public ActionResult<Book> GetSingleBook(int id)
    {
        return Books[0];
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteBook(int id)
    {
        var book = Books.FirstOrDefault(b => b.Id == id);
        if (book is null)
            return NotFound();
        Books.Remove(book);
        return NoContent();
    }

}
