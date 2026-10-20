using BookStore.Core.Models;
using BookStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.Repositories;

public class BooksRepository(BookStoreDbContext context) : IBooksRepository
{
    public async Task<List<Book>> Get()
    {
        var bookEntities = await context.Books
            .AsNoTracking()
            .ToListAsync();

        var books = bookEntities
            .Select(b => Book.Create(b.Id, b.Title, b.Description, b.Price).Book)
            .ToList();
        
        return books;
    }

    public async Task<Book> GetById(Guid id)
    {
        var bookEntity = await context.Books.FindAsync(id);

        if (bookEntity == null)
        {
            throw new KeyNotFoundException();
        }
        
        var result = Book.Create(
            bookEntity.Id,
            bookEntity.Title,
            bookEntity.Description,
            bookEntity.Price
        );

        return result.Book;
    }

    public async Task<Guid> Create(Book book)
    {
        var bookEntity = new BookEntity
        {
            Id = book.Id,
            Title = book.Title,
            Description = book.Description,
            Price = book.Price,
        };

        await context.Books.AddAsync(bookEntity);
        await context.SaveChangesAsync();
        
        return bookEntity.Id;
    }

    public async Task<Guid> Update(Guid id, string? title, string? description, decimal? price)
    {
        await context.Books
            .Where(b => b.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(b => b.Title, b => title)
                .SetProperty(b => b.Description, b => description)
                .SetProperty(b => b.Price, b => price)
            );

        return id;
    }

    public async Task<Guid> Delete(Guid id)
    {
        await context.Books
            .Where(b => b.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}