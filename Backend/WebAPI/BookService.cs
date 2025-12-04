namespace WebAPI;

using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// Service providing in-memory book operations.
/// </summary>
public class BookService : IBookService
{
    private static List<Book> _books = new()
    {
        new Book
        {
            Id = 1,
            Title = "Clean Code",
            Author = "Robert C. Martin",
            PublishedDate = "2008-08-01",
            Price = 32.95m,
            IsAvailable = true
        },
        new Book
        {
            Id = 2,
            Title = "The Pragmatic Programmer",
            Author = "Andrew Hunt, David Thomas",
            PublishedDate = "2020-09-13",
            Price = 39.99m,
            IsAvailable = true
        },
        new Book
        {
            Id = 3,
            Title = "Design Patterns",
            Author = "Erich Gamma, Richard Helm, Ralph Johnson, John Vlissides",
            PublishedDate = "1994-10-31",
            Price = 54.99m,
            IsAvailable = false
        },
        new Book
        {
            Id = 4,
            Title = "Refactoring",
            Author = "Martin Fowler",
            PublishedDate = "2018-11-20",
            Price = 47.99m,
            IsAvailable = true
        },
        new Book
        {
            Id = 5,
            Title = "Domain-Driven Design",
            Author = "Eric Evans",
            PublishedDate = "2003-08-20",
            Price = 59.99m,
            IsAvailable = true
        },
        new Book
        {
            Id = 6,
            Title = "You Don't Know JS",
            Author = "Kyle Simpson",
            PublishedDate = "2015-01-14",
            Price = 24.99m,
            IsAvailable = true
        },
        new Book
        {
            Id = 7,
            Title = "C# in Depth",
            Author = "Jon Skeet",
            PublishedDate = "2019-03-30",
            Price = 44.99m,
            IsAvailable = false
        },
        new Book
        {
            Id = 8,
            Title = "Effective TypeScript",
            Author = "Dan Vanderkam",
            PublishedDate = "2019-10-31",
            Price = 42.99m,
            IsAvailable = true
        }
    };

    private static int _nextId = 9;

    /// <summary>
    /// Returns all books.
    /// </summary>
    /// <returns>A list of all books.</returns>
    public Task<IList<Book>> GetAllBooksAsync()
    {
        return Task.FromResult<IList<Book>>(_books);
    }

    /// <summary>
    /// Gets a book by its identifier.
    /// </summary>
    /// <param name="id">The book id.</param>
    /// <returns>The book if found; otherwise null.</returns>
    public Task<Book?> GetBookByIdAsync(int id)
    {
        return Task.FromResult<Book?>(_books.FirstOrDefault(b => b.Id == id));
    }

    /// <summary>
    /// Checks whether a title already exists.
    /// </summary>
    /// <param name="title">The title to check.</param>
    /// <returns>True if a book with the given title exists (excluding excludeId), otherwise false.</returns>
    public Task<bool> TitleExistsAsync(string title)
    {
        return Task.FromResult(_books.Any(b => b.Title == title));
    }

    /// <summary>
    /// Creates a new book and assigns it an id.
    /// </summary>
    /// <param name="book">The book to create.</param>
    /// <returns>The created book (with id).</returns>
    public Task<Book> CreateBookAsync(BookDto book)
    {
        var created = new Book
        {
            Id            = _nextId++,
            Title         = book.Title,
            Author        = book.Author,
            PublishedDate = book.PublishedDate,
            Price         = book.Price,
            IsAvailable   = book.IsAvailable
        };
    
        _books.Add(created);
        return Task.FromResult(created);
    }


    /// <summary>
    /// Updates an existing book.
    /// </summary>
    /// <param name="id">The id of the book to update.</param>
    /// <param name="book">The book data to update.</param>
    /// <returns>The updated book if found; otherwise null.</returns>
    public Task<Book?> UpdateBookAsync(int id, BookDto book)
    {
        if (_books.FirstOrDefault(b => b.Id == id) == null)
        {
            return Task.FromResult<Book?>(null);
        }
        
        var updated = new Book()
        {
            Id            = id,
            Title         = book.Title,
            Author        = book.Author,
            PublishedDate = book.PublishedDate,
            Price         = book.Price,
            IsAvailable   = book.IsAvailable
        };

        _books[id - 1] = updated;
        
        return Task.FromResult<Book?>(updated);
    }

    /// <summary>
    /// Deletes a book by id.
    /// </summary>
    /// <param name="id">The id of the book to delete.</param>
    /// <returns>True if the book was removed; otherwise false.</returns>
    public Task<bool> DeleteBookAsync(int id)
    {
        return Task.FromResult(_books.RemoveAll(b => b.Id == id) > 0);
    }
}
