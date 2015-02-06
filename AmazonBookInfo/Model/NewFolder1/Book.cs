using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonBookInfo.Model
{
    class Book
    {
        public int BookId { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public Nullable<int> Comments { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<int> BestSellersRank { get; set; }
        public string Categories { get; set; }
        public Nullable<System.DateTime> PublicationDate { get; set; }
    }
    public class Category
    {
        public string Name{get;set;}
    }
}
