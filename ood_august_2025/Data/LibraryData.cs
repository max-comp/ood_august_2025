using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity; //for entity framework

namespace ood_august_2025.Data
{
    //code first entity framework class to represent the library data context
    public class LibraryData: DbContext

    {
        public LibraryData(): base("OODExam_MaxBatrak")
        {
            
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }


    }
}
