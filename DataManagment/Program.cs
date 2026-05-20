using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ood_august_2025;
using ood_august_2025.Data;


namespace DataManagment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LibraryData libraryData = new LibraryData();

            using (libraryData)
            {
                //create a new book
                Book book = new Book()
                {
                    Title = "The Great Gatsby",
                    Author = "F. Scott Fitzgerald",
                };
                //add the book to the database
                libraryData.Books.Add(book);
                //save the changes to the database
                libraryData.SaveChanges();

                //create a new borrower
                Borrower borrower = new Borrower()
                {
                    FirstName = "John",
                    Surname = "Doe",
                    StudentNumber = "12345678"
                };
                //add the borrower to the database
                libraryData.Borrowers.Add(borrower);
                //save the changes to the database
                libraryData.SaveChanges();

                //Create a borrowing record
                BorrowingRecord borrowingRecord = new BorrowingRecord()
                {
                    BookId = book.BookId,
                    BorrowerId = borrower.BorrowerId,
                    BorrowedOn = new DateTime(2025, 8, 15),
                    DueDate = new DateTime(2025, 8, 30),
                };
                //add the borrowing record to the database
                libraryData.Set<BorrowingRecord>().Add(borrowingRecord);
                //save the changes to the database
                libraryData.SaveChanges();

                Console.WriteLine("Data has been successfully added to the database.");



            }
        }
    }
}
