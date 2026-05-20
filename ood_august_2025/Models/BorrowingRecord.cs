using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ood_august_2025
{
    //class to draw relationship between book and borrower
    public class BorrowingRecord
    {
        //properties
        public int BorrowingRecordId { get; set; }  // Primary key
        
        // Foreign keys
        public int BookId { get; set; }
        public int BorrowerId { get; set; }
        
        // Navigation properties
        public Book Book { get; set; }
        public Borrower Borrower { get; set; }
        
        // Properties
        public DateTime BorrowedOn { get; set; }
        public DateTime ReturnedOn { get; set; }
        public DateTime DueDate { get; set; }




        //constructor
    }
}
