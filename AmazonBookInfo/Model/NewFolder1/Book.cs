using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonBookInfo.Model
{
    class Book
    {
        public int BookID { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int? Comments { get; set; }
        public double? Price { get; set; }
        public int? BestSellersRank { get; set; }
        public List<string> Categories { get; set; }
        public DateTime? PublicationDate { get; set; }
    }
    public class Category
    {
        public string Name{get;set;}
    }
}
