using Microsoft.AspNetCore.Mvc;
using Models;

[Route("books")]
[ApiController]
public class BookController : ControllerBase
{
    //Get retrive all Book
    [HttpGet("GetAllBook")]
    public List<Book> GetAllBook() => Book.bookList; //get all Book

    
    //Get by ID: retrive a single Book by id
    [HttpGet("GetById/{id}")]

    public ActionResult<Book> GetById(int id) //retrive specific Book by ID in parenthesis -> pass number and retrive specific Book
    {
        Book Book = Book.FindBookById(id);

        return Book != null ? Ok(Book) : NotFound(); //if it not null return Book OK esle NotFound
    }

    [HttpPost("CreateBook")]
    public ActionResult<Book> Create(Book createBook)
    {
        Book book = new Book();

        book.InputValidation(createBook);

        book.Id = Book.bookList.Count + 1;
        book.Title = createBook.Title;
        book.Body = createBook.Body;

        Book.bookList.Add(book);

        return Ok(book);
    }

    [HttpPut("UpdateBook/{id}")]

    public ActionResult Update(int id, Book updateBook) //retrive specific Book by ID in parenthesis -> pass number and retrive specific Book
    {
        Book Book = Book.FindBookById(id);
        
        if (Book == null)
        {
            return NotFound();
        }
        else
        {
            Book.Title = updateBook.Title;
            Book.Body = updateBook.Body;
        }

        return Ok(Book);
    }

    [HttpDelete("DeleteBook/{id}")]

    public ActionResult Update(int id) //retrive specific Book by ID in parenthesis -> pass number and retrive specific Book
    {
        Book Book = Book.FindBookById(id);
        
        if (Book == null) return NotFound();

        Book.bookList.Remove(Book);

        return NoContent();
    }
}
