using System;
using System.Collections.Generic;

namespace Book
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Book book1 = new Book() { ISBN = "978-5-8459-2087-4 ", AuthorName = "Joseph Albahari and Веn Albahari", Title = "C# 6.0 in a nutshell", Publisher = "O'Reilly Media", Year = 2015, NumberOfPpages = 1040, Price = 50M };
            Book book2 = new Book() { ISBN = "978-0-7356-6745-7", AuthorName = "Jeffrey Richter", Title = "CLR via C#", Publisher = "Microsoft Press", Year = 2015, NumberOfPpages = 825, Price = 60M };
            var bookList = new BookListService(new List<Book>());
            bookList.AddBook(book1);
            bookList.AddBook(book2);

            foreach (Book b in bookList.FindBookByTag("CLR via C#"))
            {
                Console.WriteLine(b.AuthorName);
            }

            Console.WriteLine(string.Format(new BookFormatProvider(), "{0:A}", book1));
            Console.WriteLine(string.Format(new BookFormatProvider(), "{0:E}", book2));
            Console.WriteLine(book2.ToString());
            Console.ReadLine();
        }
    }
}
