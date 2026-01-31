using BookStore.Api.Common;
using BookStore.Api.Data;
using BookStore.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

/// <summary>Simple loan system: loan and return books. In-memory store.</summary>
public class LoansController : BaseController
{
    /// <summary>Get all loans (active and returned).</summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<LoanModel>), StatusCodes.Status200OK)]
    public ActionResult<List<LoanModel>> GetLoans()
    {
        var result = InMemoryLoanStore.GetAll();
        return Ok(result);
    }

    /// <summary>Get a single loan by id. Returns 404 if not found.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(LoanModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<LoanModel> GetLoanById(int id)
    {
        var result = InMemoryLoanStore.GetById(id);
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    /// <summary>Loan a book. Returns 404 if book not found, 400 if book is already on loan.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(LoanModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<LoanModel> CreateLoan([FromBody] LoanRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.BorrowerName))
            return BadRequest("Borrower name is required.");
        if (InMemoryBookStore.GetById(request.BookId) is null)
            return NotFound("Book not found.");
        if (InMemoryLoanStore.IsBookOnLoan(request.BookId))
            return BadRequest("This book is already on loan.");

        var loan = InMemoryLoanStore.Create(request.BookId, request.BorrowerName);
        return Ok(loan);
    }

    /// <summary>Return a book (mark loan as returned). Returns 404 if loan not found, 400 if already returned.</summary>
    [HttpPost("{id:int}/return")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult ReturnLoan(int id)
    {
        var loan = InMemoryLoanStore.GetById(id);
        if (loan is null)
            return NotFound("Loan not found.");
        if (loan.ReturnedAt is not null)
            return BadRequest("This loan has already been returned.");

        InMemoryLoanStore.Return(id);
        return NoContent();
    }
}
