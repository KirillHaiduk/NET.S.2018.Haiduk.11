using NUnit.Framework;

namespace Book
{
    [TestFixture]
    public class BookTests
    {
        [Test]
        public void BookTest1()
        {
            Book book1 = new Book() { ISBN = "978-5-8459-2087-4", AuthorName = "Joseph Albahari and Веn Albahari", Title = "C# 6.0 in a nutshell", Publisher = "O'Reilly Media", Year = 2015, NumberOfPpages = 1040, Price = 50M };
            string formattedWithA = "Joseph Albahari and Веn Albahari, C# 6.0 in a nutshell";
            string formattedWithB = "Joseph Albahari and Веn Albahari, C# 6.0 in a nutshell, O'Reilly Media, 2015";
            string formattedWithC = "ISBN 978-5-8459-2087-4, Joseph Albahari and Веn Albahari, C# 6.0 in a nutshell, O'Reilly Media, 2015, P. 1040";
            string formattedWithD = "Joseph Albahari and Веn Albahari, C# 6.0 in a nutshell, O'Reilly Media, 2015";
            string formattedWithE = "Joseph Albahari and Веn Albahari, C# 6.0 in a nutshell, O'Reilly Media, 2015, 50$";
            Assert.AreEqual(formattedWithA, string.Format(new BookFormatProvider(), "{0:A}", book1));
            Assert.AreEqual(formattedWithB, string.Format(new BookFormatProvider(), "{0:B}", book1));
            Assert.AreEqual(formattedWithC, string.Format(new BookFormatProvider(), "{0:C}", book1));
            Assert.AreEqual(formattedWithD, string.Format(new BookFormatProvider(), "{0:D}", book1));
            Assert.AreEqual(formattedWithE, string.Format(new BookFormatProvider(), "{0:E}", book1));
        }        
    }
}
