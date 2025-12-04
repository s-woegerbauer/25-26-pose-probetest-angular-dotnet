namespace WebAPI;

public record BookDto(
 string Title,
 string Author,
 string PublishedDate,
 decimal Price,
 bool IsAvailable
 );