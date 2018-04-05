using System;

namespace Book
{
    /// <summary>
    /// Class that describes type "Book"
    /// </summary>
    public class Book : IComparable<Book>, IEquatable<Book>, IComparable
    {        
        /// <summary>
        /// Property that contains ISBN of book
        /// </summary>
        public string ISBN { get; set; }

        /// <summary>
        /// Property that contains Author name of book
        /// </summary>
        public string AuthorName { get; set; }

        /// <summary>
        /// Property that contains Title of book
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Property that contains Publisher of book
        /// </summary>
        public string Publisher { get; set; }

        /// <summary>
        /// Property that contains Year of publishing of book
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Property that contains Number of pages of book
        /// </summary>
        public int NumberOfPpages { get; set; }

        /// <summary>
        /// Property that contains Price of book
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Implementing interface IComparable<Book> to define a generalized type-specific comparison method that class Book implements to order or sort its instances
        /// </summary>
        /// <param name="other">A Book to compare with this instance</param>
        /// <returns>A value that indicates the relative order of the objects being compared</returns>
        public int CompareTo(Book other)
        {
            if (other is null)
            {
                return 1;
            }

            if (Price > other.Price)
            {
                return 1;
            }

            if (Price < other.Price)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Implementing interface IComparable to define a generalized type-specific comparison method that class Book implements to order or sort its instances
        /// </summary>
        /// <param name="obj">An object to compare with this instance</param>
        /// <returns>A value that indicates the relative order of the objects being compared</returns>
        public int CompareTo(object obj)
        {
            if (obj is null || obj.GetType() != GetType())
            {
                return 1;
            }

            if (ReferenceEquals(this, obj))
            {
                return 0;
            }

            return CompareTo((Book)obj);
        }

        /// <summary>
        /// Implementing interface IEquatable<Book> to indicate whether the current object is equal to another object of the same type
        /// </summary>
        /// <param name="other">A book to compare with this book</param>
        /// <returns>true if the current book is equal to other; otherwise, false</returns>
        public bool Equals(Book other)
        {
            if (other is null)
            {
                return false;
            }

            if (ISBN == other.ISBN)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Overriding the System.Object method to give string representation of Book instance
        /// </summary>
        /// <returns>String representation of Book instance</returns>
        public override string ToString() => string.Format($"ISBN {ISBN}, {AuthorName}, {Title}, {Publisher}, {Year}, P. {NumberOfPpages}");
    }
}
