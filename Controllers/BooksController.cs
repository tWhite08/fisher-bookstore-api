using Fisher.Bookstore.Services;
using Fisher.Bookstore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fisher.Bookstore.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {

        private IBooksRepository booksRepository;

        public BooksController(IBooksRepository repository)
        {
            booksRepository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(booksRepository.GetBooks());
        }

        [HttpGet("{bookId}")]
        public IActionResult Get(int bookId)
        {
            if (!booksRepository.BookExists(bookId))
            {
                return NotFound();
            }

            return Ok(booksRepository.GetBook(bookId));
        }

        [HttpPost]
        public IActionResult Post([FromBody]Book book)
        {
            var bookId = booksRepository.AddBook(book);
            return Created($"https://localhost:5001/api/books/{bookId}", book);
        }

        [HttpDelete("{bookId}")]
        public IActionResult Delete(int bookId)
        {
            if (!booksRepository.BookExists(bookId))
            {
                return NotFound();
            }

            booksRepository.DeleteBook(bookId);
            return Ok();
        }
    }
}
