using ood_august_2025.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ood_august_2025
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LibraryData libraryData;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Initialize the database context
            libraryData = new LibraryData();
            //Query the database using LINQ for all Borrowers in the database
            var borrowers = libraryData.Borrowers.ToList();
            //Display the borrowers in the ListBox
            BorrowerListBox.ItemsSource = borrowers;
        }

        //Handle that all other books that are not borrowed by the selected borrower are displayed in the AvailableBooksListBox
        //Borrowed books are ordered by the due date, with the earliest due date first
        private void BorrowerListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            {   //Get the selected borrower
                Borrower selectedBorrower = BorrowerListBox.SelectedItem as Borrower;
                if (selectedBorrower != null)
                {
                    //Get the borrowing records for the selected borrower, ordered by due date
                    var borrowingRecords = libraryData.Set<BorrowingRecord>()
                        .Where(br => br.BorrowerId == selectedBorrower.BorrowerId)
                        .OrderBy(br => br.DueDate)
                        .ToList();
                    //Get the books that are currently borrowed by the selected borrower
                    var borrowedBooks = borrowingRecords.Select(br => libraryData.Books.Find(br.BookId)).ToList();
                    //Display the borrowed books in the BorrowedBooksListBox
                    //format as Titly - Due: DueDate( as ToString in the BorrowingRecord class)
                    BorrowedBooksListBox.ItemsSource = borrowedBooks.Select(b => $"{b.Title} - Due: {borrowingRecords.First(br => br.BookId == b.BookId).DueDate.ToShortDateString()}").ToList();

                    //Get the books that are not currently borrowed by the selected borrower
                    // Get IDs of borrowed books
                    var borrowedBookIds = borrowingRecords.Select(br => br.BookId).ToList();

                    // Get books that are NOT in the borrowed list
                    var availableBooks = libraryData.Books
                        .Where(b => !borrowedBookIds.Contains(b.BookId))
                        .ToList();
                    //Display the available books in the AvailableBooksListBox
                    AvailableBooksListBox.ItemsSource = availableBooks;
                }
            }

            }
        /*
         * Add functionality so that books can be borrowed and returned. When a book is
        borrowed the return date is automatically set to 7 days from the current date. If the
        return date is a weekend amend the return date to the following Monday
         */
        private void BorrowButton_Click(object sender, RoutedEventArgs e)
        {
            //Get the selected borrower
            Borrower selectedBorrower = BorrowerListBox.SelectedItem as Borrower;
            //Get the selected book to borrow
            Book selectedBookToBorrow = AvailableBooksListBox.SelectedItem as Book;
            if (selectedBookToBorrow != null)
            {
                //Create a new borrowing record
                BorrowingRecord newBorrowingRecord = new BorrowingRecord()
                {
                    BookId = selectedBookToBorrow.BookId,
                    BorrowerId = selectedBorrower.BorrowerId,
                    BorrowedOn = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(7)
                };
                //If the due date is a weekend, set it to the following Monday
                if (newBorrowingRecord.DueDate.DayOfWeek == DayOfWeek.Saturday)
                {
                    newBorrowingRecord.DueDate = newBorrowingRecord.DueDate.AddDays(2);
                }
                else if (newBorrowingRecord.DueDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    newBorrowingRecord.DueDate = newBorrowingRecord.DueDate.AddDays(1);
                }
                //Add the borrowing record to the database
                libraryData.BorrowingRecords.Add(newBorrowingRecord);
                libraryData.SaveChanges();
                //Refresh the lists
                BorrowerListBox_SelectionChanged(null, null);
            }

            }
        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            //Get the selected borrower
            Borrower selectedBorrower = BorrowerListBox.SelectedItem as Borrower;
            //Get the selected book to return
            string selectedBookToReturnString = BorrowedBooksListBox.SelectedItem as string;
            if (selectedBookToReturnString != null)
            {
                //Extract the book title from the selected string
                string selectedBookTitle = selectedBookToReturnString.Split(new string[] { " - Due: " }, StringSplitOptions.None)[0];
                //Find the book in the database
                Book selectedBookToReturn = libraryData.Books.FirstOrDefault(b => b.Title == selectedBookTitle);
                if (selectedBookToReturn != null)
                {
                    //Find the borrowing record for the selected book and borrower
                    BorrowingRecord borrowingRecordToRemove = libraryData.BorrowingRecords.FirstOrDefault(br => br.BookId == selectedBookToReturn.BookId && br.BorrowerId == selectedBorrower.BorrowerId);
                    if (borrowingRecordToRemove != null)
                    {
                        //Remove the borrowing record from the database
                        libraryData.BorrowingRecords.Remove(borrowingRecordToRemove);
                        libraryData.SaveChanges();
                        //Refresh the lists
                        BorrowerListBox_SelectionChanged(null, null);
                    }
                }
            }

        }
    }
            
        }

