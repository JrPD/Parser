using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonBookInfo.Model
{
    class BookContext:DbContext
    {
        public BookContext()
            : base("Model2")
        {
            
        }
        public DbSet<Book> Books { get; set; } 
    }
}
