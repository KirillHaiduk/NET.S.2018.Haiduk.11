using System;
using System.Globalization;
using NLog;

namespace Book
{
    /// <summary>
    /// Class that contains method for formatting string representation of books
    /// </summary>
    public class BookFormatProvider : IFormatProvider, ICustomFormatter
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Implementing interface ICustomFormatter to convert the value of a book to an equivalent string representation using specified format
        /// </summary>
        /// <param name="format">A format string containing formatting specifications</param>
        /// <param name="arg">An object to format</param>
        /// <param name="formatProvider">An object that supplies format information about the current instance</param>
        /// <returns>The string representation of the value of arg, formatted as specified by format and formatProvider</returns>
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg.GetType() != typeof(Book))
            {
                try
                {
                    return HandleOtherFormats(format, arg);
                }
                catch (FormatException ex)
                {
                    logger.Info("Format exception:");
                    logger.Info(ex.Message);                    
                    logger.Error(ex.StackTrace, $"Format Exception occured in BookFormatProvider.Format Method while checking {arg} format");
                    throw new FormatException(string.Format("The format of '{0}' is invalid.", format), ex);
                }
            }

            string formatString = string.Empty;
            var book = arg as Book;
            if (!string.IsNullOrEmpty(format))
            {
                formatString = format.Length > 1 ? format.Substring(0, 1) : format;
            }

            switch (formatString.ToUpper())
            {
                case "A":
                    return string.Format($"{book.AuthorName}, {book.Title}");
                case "B":
                    return string.Format($"{book.AuthorName}, {book.Title}, {book.Publisher}, {book.Year}");
                case "C":
                    return string.Format($"ISBN {book.ISBN}, {book.AuthorName}, {book.Title}, {book.Publisher}, {book.Year}, P. {book.NumberOfPpages}");
                case "D":
                    return string.Format($"{book.AuthorName}, {book.Title}, {book.Publisher}, {book.Year}");
                case "E":
                    return string.Format($"{book.AuthorName}, {book.Title}, {book.Publisher}, {book.Year}, {book.Price}$");
                default:
                    try
                    {
                        return HandleOtherFormats(format, arg);
                    }
                    catch (FormatException ex)
                    {
                        logger.Info("Format exception:");
                        logger.Info(ex.Message);
                        logger.Error(ex.StackTrace, $"Format Exception occured in BookFormatProvider.Format Method while switching {format} string");
                        throw new FormatException(string.Format("The format of '{0}' is invalid.", format), ex);
                    }
            }
        }

        /// <summary>
        /// Implementig interface IFormatProvider for getting type of object 
        /// </summary>
        /// <param name="formatType">An object that specifies the type of format object to return</param>
        /// <returns>Object that provides formatting services for the specified type</returns>
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return this;
            }
            else
            {
                return null;
            }
        }

        private string HandleOtherFormats(string format, object arg)
        {
            logger.Info("Trying to handle other farmat");
            if (arg is IFormattable)
            {
                return ((IFormattable)arg).ToString(format, CultureInfo.CurrentCulture);
            }
            else if (arg != null)
            {
                return arg.ToString();
            }
            else
            {
                return string.Empty;                
            }
        }
    }
}
