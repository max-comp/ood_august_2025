using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ood_august_2025
{
    public class Borrower
    {
        //properties
        public int BorrowerId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string StudentNumber { get; set; }

        //relationship
        public List<BorrowingRecord> BorrowingRecords { get; set; }


        //constructor
        public Borrower()
        {
            BorrowingRecords = new List<BorrowingRecord>();
        }

        //methods
    }
}
