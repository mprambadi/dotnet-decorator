using System.Collections.Generic;
using System;

namespace dotnet_decorator
{
    class Program
    {
        static void Main(string[] args)
        {
            var book = new Book("John", "hello john", 10);
            book.Display();

            var borrow = new Borrowable(book);

            borrow.BorrowItem("Johny");
            borrow.BorrowItem("Johna");

            borrow.Display();

        }
    }

    interface Hello 
    {
        void world();
    }

    interface SayHi 
    {
        void sayHello();
    }

    interface SayMyName
    {
        void sayName();
    }

    class World : Hello, SayHi, SayMyName
    {
        public void sayHello()
        {
            throw new NotImplementedException();
        }

        public void sayName()
        {
            throw new NotImplementedException();
        }

        public void world()
        {
            throw new NotImplementedException();
        }
    }

    abstract class LibraryTwo
    {
        public abstract void Display();
    }


    abstract class LibraryItem
    {
        public int NumCopies { get; set; }
        public abstract void Display();
    }

    class Book : LibraryItem, SayHi
    {
        private string _author;
        private string _title;

        public Book(string author, string title, int numCopies)
        {
            _author = author;
            _title = title;
            NumCopies = numCopies;
        }
        public override void Display()
        {
            Console.WriteLine("Book......");
            Console.WriteLine($"Author {_author}");
            Console.WriteLine($"title {_title}");
            Console.WriteLine($"Copies {NumCopies}");
        }

        public void sayHello()
        {
            throw new NotImplementedException();
        }
    }


    abstract class Decorator : LibraryItem
    {
        protected LibraryItem libraryItem;

        public Decorator(LibraryItem library)
        {
            libraryItem = library;
        }
        public override void Display()
        {
            libraryItem.Display();
        }
    }

    class Borrowable : Decorator
    {
        protected List<string> borrowers = new List<string>();

        public Borrowable(LibraryItem library) : base(library) { }

        public void BorrowItem(string name)
        {
            borrowers.Add(name);
            libraryItem.NumCopies--;
        }

        public void ReturnItem(string name)
        {
            borrowers.Remove(name);
            libraryItem.NumCopies++;
        }
        public override void Display()
        {
            base.Display();

            foreach (var item in borrowers)
            {
                Console.WriteLine($"borrower {item}");
            }
        }
    }
}
