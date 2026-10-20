using BookStore.Core.Models;

namespace BookStore.Core.Abstractions;

public interface IBookService
{
    Task<List<Book>> GetAllBooks();
    Task<Book?> GetBookById(Guid id);
    Task<Guid> CreateBook(Book book);
    Task<Guid> UpdateBook(Guid id, string title, string description, decimal price);
    Task<Guid> DeleteBook(Guid id);
}