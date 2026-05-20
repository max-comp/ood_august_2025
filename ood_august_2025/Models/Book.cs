using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ood_august_2025
{
    public class Book
    {
        //properties
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        //relationship
        public List<BorrowingRecord> BorrowingRecords { get; set; }


        //constructor
        public Book()
        {
            BorrowingRecords = new List<BorrowingRecord>();
        }


        //methods
    }
}
