namespace Models{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        
        public static List<Book> bookList = new List<Book>
        {
            new Book {Id = 1, Title = "Title 1", Body = "Body 1"},
            new Book {Id = 2, Title = "Title 2", Body = "Body 2"},
            new Book {Id = 3, Title = "Title 3", Body = "Body 3"}
        };

        public static Book FindBookById(int id)
        {
            Book Book = bookList.FirstOrDefault(u => u.Id == id);

            return Book;
        }

        public static Book FindBookByTitle(string title)
        {
            Book Book = bookList.FirstOrDefault(u => u.Title == title);

            return Book;
        }

        public void InputValidation(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title))
            {
                throw new Exception("Title is null");
            }

            if (string.IsNullOrWhiteSpace(book.Body))
            {
                throw new Exception("Body is null");
            }
        }
    }
}
