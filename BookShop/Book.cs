using System;

namespace BookShop
{
    public sealed class Book: Publication
    {
        public Book(string title, string author, string publisher) :
            this(title, String.Empty, author, publisher)
        { }

        public Book(string title, string isbn, string author, string publisher) :base(title, publisher, PublicationType.Book)
        {
            //isbn argument must be a 10- or 13-character numeric string without "-" characters.
            // We could also determine whether the ISBN is valid by comparing its checksum digit
            // with a computed checksum.
            //

            if (!String.IsNullOrEmpty(isbn))
            {
                //Determine if ISBN length
                if (!(isbn.Length == 10 | isbn.Length == 13))
                    throw new ArgumentException("The ISBN must be a 10- or 13-character numeric string.");
                ulong nISBN = 0;
                if (!UInt64.TryParse(isbn, out nISBN))
                    throw new ArgumentException("The ISBN can consist of numeric characters only.");
            }
            ISBN = isbn;

            Author = author;
        }

        public string ISBN { get; }

        public string Author { get; }

        public Decimal Price { get; private set; }

        // A three-digit ISO currency symbol.
        public string Currency { get; private set; }

        //Returns the old price, and sets a new price.
        public Decimal SetPrice(Decimal price, string currnecy)
        {
            if (price < 0)
                throw new ArgumentOutOfRangeException("The price cannot be negative.");
            Decimal oldValue = Price;
            Price = price;

            if (currnecy.Length != 3)
                throw new ArithmeticException("The ISO currency symbol is a 3-character string.");
            Currency = currnecy;

            return oldValue;
        }

        public override bool Equals(object obj)
        {
            Book book = obj as Book;
            if (book == null)
                return false;
            else
                return ISBN == book.ISBN;
        }

        public override int GetHashCode() => ISBN.GetHashCode();

        public override string ToString() => $"{(String.IsNullOrEmpty(Author) ? "" : Author + ", ")}{Title}";
    }
}
