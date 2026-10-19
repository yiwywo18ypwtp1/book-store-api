using BookStore.Core.Models;
using BookStore.DataAccess.Repositories;

namespace BookStore.Application;

public class BookService(IBooksRepository booksRepository) : IBookService
{
    public async Task<List<Book>> GetAllBooks()
    {
        return await booksRepository.Get();
    }
    
    public async Task<Guid> CreateBook(Book book)
    {
        return await booksRepository.Create(book);
    }
    
    public async Task<Guid> UpdateBook(Guid id, string title, string description, decimal price)
    {
        return await booksRepository.Update(id, title, description, price);
    }
    
    public async Task<Guid> DeleteBook(Guid id)
    {
        return await booksRepository.Delete(id);
    }
}