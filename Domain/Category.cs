using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Category
    {
        public long Id { get; set; }
        public string CategoryName { get; set; }
        public ICollection<Category> Categories { get; set;}

    }
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public Category Category { get; set; }  
        public long CategoryId { get; set; }
    }
}
