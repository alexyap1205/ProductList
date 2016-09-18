using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace InnoDataAccess
{
    public class ProductsAccessor : IProductsAccessor
    {
        public ProductsAccessor()
        {
            DbConnectionHelper = new SQLDBConnectionHelper();
        }

        internal IDBConnectionHelper DbConnectionHelper { get; set; }

        public List<Product> GetProducts()
        {
            List<Product> productsList = new List<Product>();

            SqlConnection connection = DbConnectionHelper.CreateConnection();

            if (connection != null)
            {
                SqlDataReader reader = DbConnectionHelper.CreateReader(SqlStatements.SelectAllProducts, connection);

                if (reader != null && reader.HasRows)
                {
                    Dictionary<int, Brand> brands = new Dictionary<int, Brand>();
                    Dictionary<int, Category> categories = new Dictionary<int, Category>();

                    while (reader.Read())
                    {
                        // Add Products to List
                        int brandId = reader.GetInt32(1);
                        int categoryId = reader.GetInt32(2);

                        Brand brand = null;
                        if (!brands.TryGetValue(brandId, out brand))
                        {
                            brand = new Brand()
                            {
                                ID = brandId,
                                CompanyName = reader.GetString(10),
                                Name = reader.GetString(9)
                            };
                            brands[brandId] = brand;
                        }

                        Category category = null;
                        if (!categories.TryGetValue(categoryId, out category))
                        {
                            category = new Category()
                            {
                                ID = categoryId,
                                ImageFile = reader.GetString(12),
                                Name = reader.GetString(11)
                            };
                            categories[categoryId] = category;
                        }

                        PropertyDetail[] propertyDetails = new PropertyDetail[0];
                        string details = reader.IsDBNull(8) ? String.Empty : reader.GetString(8);
                        if (!String.IsNullOrEmpty(details))
                        {
                            JavaScriptSerializer serializer = new JavaScriptSerializer();
                            propertyDetails = serializer.Deserialize<PropertyDetail[]>(details);
                        }

                        Product product = new Product()
                        {
                            Brand = brand,
                            Category = category,
                            Description = reader.GetString(6),
                            ID = reader.GetInt32(0),
                            ImageFile = reader.GetString(7),
                            Name = reader.GetString(3),
                            Price = reader.GetFloat(4),
                            Summary = reader.GetString(5),
                            Featured = reader.GetBoolean(13),
                            PropertyDetails = new List<PropertyDetail>(propertyDetails)
                        };

                        productsList.Add(product);

                    }

                    DbConnectionHelper.DisposeReader(reader);
                }

                DbConnectionHelper.DisposeConnection(connection);
            }

            return productsList;
        }

        public List<Product> GetProducts(Category category)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProducts(Brand brand)
        {
            throw new NotImplementedException();
        }
    }
}
