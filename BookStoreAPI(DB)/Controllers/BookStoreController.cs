using Microsoft.AspNetCore.Mvc;
using BookStoreApi.Models;
using BookStoreApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookBL _bookBL;

        public BooksController(BookBL bookBL)
        {
            _bookBL = bookBL;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks([FromQuery] string title = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var filteredBooks = _bookBL.GetBooks()
                .Where(b => string.IsNullOrEmpty(title) || b.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            bool hasMore = filteredBooks.Count >= pageSize;
            return Ok(new { books = filteredBooks, hasMore });
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = _bookBL.GetBook(id);
            if (book == null)
                return NotFound(new { Message = "Book not found" });
            return Ok(book);
        }

        [HttpPost]
        public ActionResult<Book> CreateBook([FromBody] Book book)
        {
            if (book == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid book data.");
            }

            _bookBL.AddBook(book);
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book book)
        {
            var existingBook = _bookBL.GetBook(id);
            if (existingBook == null)
                return NotFound(new { Message = "Book not found" });

            _bookBL.UpdateBook(id, book);
            return Ok(new { Message = "Book updated successfully" });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _bookBL.GetBook(id);
            if (book == null)
                return NotFound(new { Message = "Book not found" });

            _bookBL.DeleteBook(id);
            return Ok(new { Message = "Book deleted successfully" });
        }

        [HttpGet("author/{author}")]
        public ActionResult<IEnumerable<Book>> GetBooksByAuthor(string author)
        {
            var books = _bookBL.GetBooks()
                .Where(b => b.Author.Equals(author, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Ok(books);
        }
        /*
        [HttpPost("contact-us")]
        public IActionResult ContactUs([FromBody] ContactFormModel contactForm)
        {
            if (contactForm == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid contact form data.");
            }

            Console.WriteLine($"Received Contact Form Submission:");
            Console.WriteLine($"Name: {contactForm.Name}");
            Console.WriteLine($"Email: {contactForm.Email}");
            Console.WriteLine($"Subject: {contactForm.Subject}");
            Console.WriteLine($"Message: {contactForm.Message}");

            AddCustomerReview(contactForm);
            return Ok("Contact form submitted successfully.");
        }

        [HttpGet("customer-reviews")]
        public ActionResult<IEnumerable<ContactFormModel>> GetCustomerReviews()
        {
            return Ok(CustomerReviews);
        }

        private static List<ContactFormModel> CustomerReviews = new List<ContactFormModel>();

        private void AddCustomerReview(ContactFormModel review)
        {
            CustomerReviews.Add(review);
        }
        */
        [HttpGet("featured-books")]
        public ActionResult<IEnumerable<Book>> GetFeaturedBooks()
        {
            var featuredBooks = _bookBL.GetFeaturedBooks(); // Fetch from BookBL

            if (featuredBooks == null || !featuredBooks.Any())
            {
                return NotFound(new { Message = "No featured books found" });
            }

            return Ok(featuredBooks);
        }

    }
}
