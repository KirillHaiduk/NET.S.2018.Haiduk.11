using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NLog;

namespace Book
{
    public class BookListService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private List<Book> bookList;

        /// <summary>
        /// Constructor to create instance of Book List
        /// </summary>
        /// <param name="books">New Book List</param>
        public BookListService(List<Book> books) => bookList = books;

        #region Methods for working with Book List

        /// <summary>
        /// Method for adding a new book to Book List
        /// </summary>
        /// <param name="book">New book to add</param>
        public void AddBook(Book book)
        {
            logger.Info("Adding new book in collection");
            try
            {
                bookList.Add(book);
            }
            catch (Exception ex)
            {
                logger.Info("Unhandled exception while adding new book:");
                logger.Info(ex.Message);
                logger.Error(ex.StackTrace);
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                if (bookList.Contains(book))
                {
                    Console.WriteLine("Book successfully added");
                }
                else
                {
                    Console.WriteLine("Book cannot be added");
                }                
            }
        }

        /// <summary>
        /// Method for removing a book from Book List
        /// </summary>
        /// <param name="book">Book to remove</param>
        public void RemoveBook(Book book)
        {
            logger.Info("Removing a book from collection");
            try
            {
                bookList.Remove(book);
            }
            catch (Exception ex)
            {
                logger.Info("Unhandled exception while removing a book:");
                logger.Info(ex.Message);
                logger.Error(ex.StackTrace);
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                if (!bookList.Contains(book))
                {
                    Console.WriteLine("Book successfully removed");
                }
                else
                {
                    Console.WriteLine("Book cannot be removed");
                }
            }
        }

        /// <summary>
        /// Method for searching a book in Book List 
        /// </summary>
        /// <param name="tag">Tag for searching a book (ISBN, Author Name or Title)</param>
        /// <returns>Collection of one or more books by tag</returns>
        public IEnumerable<Book> FindBookByTag(object tag)
        {
            IEnumerable<Book> book = null;
            try
            {
                book = bookList.Where(t => t.ISBN == (string)tag || t.AuthorName == (string)tag || t.Title == (string)tag);
                return book;
            }
            catch (Exception ex)
            {
                logger.Info("Unhandled exception while searching book by tag:");
                logger.Info(ex.Message);
                logger.Error(ex.StackTrace);
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                if (book == null)
                {
                    Console.WriteLine("Book not found");
                }
            }
        }

        /// <summary>
        /// Method for sorting books in Book List 
        /// </summary>
        /// <param name="tag">Tag for sorting a book (ISBN, Author Name or Title)</param>
        /// <returns>Sorted collection of books by tag</returns>
        public void SortBooksByTag(object tag) => bookList.OrderBy(t => t.ISBN == (string)tag || t.AuthorName == (string)tag || t.Title == (string)tag || t.Price == (decimal)tag);

        #endregion

        #region Methods for working with Binary Repository (BookListStorage)

        /// <summary>
        /// Method for adding a new book to Book Binary Repository
        /// </summary>
        /// <param name="s">Stream for working with Binary Repository</param>
        /// <param name="book">New book to add in Binary Repository</param>
        public void SaveBook(Stream s, Book book)
        {
            var w = new BinaryWriter(s);
            try
            {
                w.Write(book.ISBN);
                w.Write(book.AuthorName);
                w.Write(book.Title);
                w.Write(book.Publisher);
                w.Write(book.Year);
                w.Write(book.NumberOfPpages);
                w.Write(book.Price);
            }
            catch (IOException ex)
            {
                logger.Info("IO exception while adding book in Binary Repository (BookListStorage):");
                logger.Info(ex.Message);
                logger.Error(ex.StackTrace);
                Console.WriteLine(ex.Message);
                throw;
            }
            catch (ArgumentNullException ex)
            {
                logger.Info("ArgumentNullException while adding book in Binary Repository (BookListStorage):");
                logger.Info(ex.Message);
                logger.Error(ex.StackTrace);
                Console.WriteLine(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                logger.Info("Unhandled exception while adding book in Binary Repository (BookListStorage):");
                logger.Info(ex.Message);
                logger.Error(ex.StackTrace);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                w.Flush();
            }
        }

        /// <summary>
        /// Method for loading a book from Book Binary Repository
        /// </summary>
        /// <param name="s">Stream for working with Binary Repository</param>
        public void LoadBook(Stream s)
        {
            var r = new BinaryReader(s);
            var book = new Book();
            try
            {
                book.ISBN = r.ReadString();
                book.AuthorName = r.ReadString();
                book.Title = r.ReadString();
                book.Publisher = r.ReadString();
                book.Year = r.ReadInt32();
                book.NumberOfPpages = r.ReadInt32();
                book.Price = r.ReadDecimal();
            }
            catch (EndOfStreamException ex)
            {
                logger.Info("EndOfStreamException while loading book from Binary Repository (BookListStorage):");
                logger.Info(ex.Message);
                logger.Error(ex.StackTrace);
                Console.WriteLine(ex.Message);
                throw;
            }
            catch (IOException ex)
            {
                logger.Info("IOException while loading book from Binary Repository (BookListStorage):");
                logger.Info(ex.Message);
                logger.Error(ex.StackTrace);
                Console.WriteLine(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                logger.Info("Unhandled exception while loading book from Binary Repository (BookListStorage):");
                logger.Info(ex.Message);
                logger.Error(ex.StackTrace);
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                Console.WriteLine("Book not found");
            }
        }

        #endregion
    }
}
