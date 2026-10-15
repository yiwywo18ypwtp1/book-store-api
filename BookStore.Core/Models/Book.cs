namespace BookStore.Core.Models;

public class Book
{
    public Book(Guid id, string title, string description, decimal price )
    {
        Id = id;
        Title = title;
        Description = description;
        Price = price;
    }
    
    public Guid Id { get; }
    
    public string Title { get; } = string.Empty;
    
    public string Description { get; } = string.Empty;
    
    public decimal Price { get; }
}