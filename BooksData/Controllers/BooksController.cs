using BooksData.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooksData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly DataContext context;

        public BooksController(DataContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetAllBooks()
        {
            return Ok(await context.Books.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            var book = await context.Books.FindAsync(id);
            if (book == null)
                return BadRequest("Book not found!");
            return Ok(book);
        }
        [HttpPost]
        public async Task<ActionResult<List<Book>>> AddBook(Book book)
        {
            context.Books.Add(book);
            await context.SaveChangesAsync();
            return Ok(await context.Books.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Book>>> UpdateBook(Book request)
        {
            var book = await context.Books.FindAsync(request.Id);
            if (book == null)
                return BadRequest("Book not found!");
            book.Title = request.Title;
            book.Author = request.Author;
            book.Year = request.Year;
            await context.SaveChangesAsync();
            return Ok(await context.Books.ToListAsync());
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult<List<Book>>> DeleteBook(int Id)
        {
            var book = await context.Books.FindAsync(Id);
            if (book == null)
                return BadRequest("Book not found!");
            context.Books.Remove(book);
            await context.SaveChangesAsync();
            return Ok(await context.Books.ToListAsync());
        }

    }
}
