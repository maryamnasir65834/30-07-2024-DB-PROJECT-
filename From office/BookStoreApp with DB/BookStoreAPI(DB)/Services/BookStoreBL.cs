using BookStoreApi.DataAccessLayer;
using BookStoreApi.Models;

namespace BookStoreApi.Services
{
    public class BookBL
    {
        private readonly BookDAL _bookDAL;

        public BookBL(BookDAL bookDAL)
        {
            _bookDAL = bookDAL;
        }

        public List<Book> GetBooks()
        {
            return _bookDAL.GetBooks();
        }

        public Book GetBook(int id)
        {
            return _bookDAL.GetBook(id);
        }

        public void AddBook(Book book)
        {
            _bookDAL.AddBook(book);
        }

        public void UpdateBook(int id, Book book)
        {
            var existingBook = _bookDAL.GetBook(id);
            if (existingBook != null)
            {
                book.Id = id; // Ensure the ID remains unchanged
                _bookDAL.UpdateBook(book);
            }
        }

        public void DeleteBook(int id)
        {
            _bookDAL.DeleteBook(id);
        }

        public List<Book> GetFeaturedBooks()
        {
            return _bookDAL.GetFeaturedBooks();
        }

    }
}
