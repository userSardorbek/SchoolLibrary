using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Dto.BookDto;
using WebApplication1.Dto.GenreDto;
using WebApplication1.Interfaces;
using WebApplication1.Mapper;
using WebApplication1.Model;

namespace WebApplication1.Service;

public class BookService : IBookRepository
{
    private readonly ApplicationDbContext _context;

    public BookService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<BookDto?> GetByIdAsync(Guid id)
    {
        var modelBookDto = await _context.Books.Where(a => a.Id == id)
            .Select(a => new BookDto()
            {
                Id = a.Id,
                Author = a.Author,
                Title = a.Title,
                ISBN = a.ISBN,
                Availability = a.Availability,
                Genres = _context.Genres
                    .Where(g => g.BookGenres.Any(bg => bg.BookId == id))
                    .Select(g => new GenreDto() { Id = g.Id, Name = g.Name })
                    .ToList()
            }).FirstOrDefaultAsync();

        return modelBookDto ?? null;
    }

    public async Task<BookDto?> CreateBook(BookCreateDto createModel)
    {
        var genresDto = new List<GenreDto>();
        var book = new Book() { Title = createModel.Title, Author = createModel.Author, ISBN = createModel.ISBN };
        await _context.Books.AddAsync(book);

        for (var i = 0; i < createModel.GenreId.Length; i++)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(x => x.Id == createModel.GenreId[i]);
            if (genre == null)
                continue;

            var bookGenre = new BookGenre() { BookId = book.Id, GenreId = genre.Id };
            book.BookGenres.Add(bookGenre);
            await _context.BookGenres.AddAsync(bookGenre);
            genresDto.Add(genre.GenreToGenreDto());
        }

        await _context.SaveChangesAsync();
        return book.BookToBookDto(genresDto);
    }

    public async Task<BookDto?> EditBook(Guid bookId, BookEditDto model)
    {
        var book = await _context.Books.FindAsync(bookId);
        if (book == null)
            return null;
        var genresDto = new List<GenreDto>();
        book.Title = model.Title;
        book.Author = model.Author;
        book.Availability = model.Availability;
        book.ISBN = model.ISBN;
        for (var i = 0; i < model.GenreId.Length; i++)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(x => x.Id == model.GenreId[i]);
            if (genre == null)
                continue;
            var bookGenre = new BookGenre() { BookId = book.Id, GenreId = genre.Id };
            book.BookGenres.Add(bookGenre);
            await _context.BookGenres.AddAsync(bookGenre);
            genresDto.Add(genre.GenreToGenreDto());
        }

        await _context.SaveChangesAsync();

        return book.BookToBookDto(genresDto);
    }

    public async Task<BookDto?> DeleteBook(Guid bookId)
    {
        var bookModel = await _context.Books.FindAsync(bookId);
        if (bookModel == null)
            return null;
        _context.Remove(bookModel);
        await _context.SaveChangesAsync();
        return bookModel.BookToBookDto(null);
    }
}