using BookStore.Api.Data;
using BookStore.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

/// <summary>Authors API: in-memory store, no database. Id, Name.</summary>
[ApiController]
[Route("/authors")]
public class AuthorsController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(List<AuthorModel>), StatusCodes.Status200OK)]
    public ActionResult<List<AuthorModel>> GetAuthors()
    {
        var result = InMemoryAuthorStore.GetAll();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(AuthorModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<AuthorModel> GetAuthorById(int id)
    {
        var result = InMemoryAuthorStore.GetById(id);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(AuthorModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<AuthorModel> CreateAuthor([FromBody] AuthorRequest request)
    {
        var name = request.Name?.Trim() ?? string.Empty;
        if (string.IsNullOrEmpty(name))
            return BadRequest("Name is required.");
        if (name.Length < 3)
            return BadRequest("Name must be at least 3 characters long.");

        var author = InMemoryAuthorStore.Add(name);
        return CreatedAtAction(nameof(GetAuthorById), new { id = author.Id }, author);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult DeleteAuthor(int id)
    {
        if (!InMemoryAuthorStore.Delete(id))
            return NotFound();
        return NoContent();
    }
}
