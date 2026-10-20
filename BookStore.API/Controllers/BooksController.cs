using BookStore.API.Contracts;
using BookStore.Application;
using BookStore.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers;

[ApiController]
[Route("[controller]")]

public class BooksController(IBookService bookService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<BooksResponse>>> GetBooks()
    {
        var books = await bookService.GetAllBooks();
        
        var response = books.Select(b => new BooksResponse(b.Id, b.Title, b.Description, b.Price));
        
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<List<BooksResponse>>> GetBookById(Guid id)
    {
        var books = await bookService.GetBookById(id);

        return Ok(books);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateBook([FromBody] BooksRequest request)
    {
        var (book, error) = Book.Create(
            Guid.NewGuid(),
            request.Title,
            request.Description,
            request.Price
        );

        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(error);
        }
        
        var bookId = await bookService.CreateBook(book);
        
        return Ok(bookId);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Guid>> UpdateBook(Guid id, [FromBody] BooksRequest request)
    {
        await bookService.UpdateBook(id, request.Title,  request.Description, request.Price);
        
        return Ok(id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Guid>> DeleteBook(Guid id)
    {
        await bookService.DeleteBook(id);
        
        return Ok(id);
    }
}