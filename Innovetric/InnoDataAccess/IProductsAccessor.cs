using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoDataAccess
{
    public interface IProductsAccessor
    {
        List<Product> GetProducts();

        List<Product> GetProducts(Category category);

        List<Product> GetProducts(Brand brand);
    }

    public class Brand
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string CompanyName { get; set; }
    }

    public class Category
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string ImageFile { get; set; }

    }

    public class Product
    {
        public int ID { get; set; }

        public Brand Brand { get; set; }

        public Category Category { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }

        public string ImageFile { get; set; }

        public bool Featured { get; set; }

        public List<PropertyDetail> PropertyDetails { get; set; }
    }

    public class PropertyDetail
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
