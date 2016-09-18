using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoDataAccess
{
    internal class SqlStatements
    {
        public const string SelectAllProducts =
            @"SELECT P.Id, P.BrandId, P.CategoryId, P.Name as ProductName, P.Price, P.Summary, P.Description, P.Image as ProductImage, P.PropertyDetails,
	            B.Name as BrandName, B.Company as CompanyName,
	            C.Name as CategoryName, C.Image as CategoryImage, P.Featured
            FROM Products P LEFT OUTER JOIN Brands B
            ON P.BrandId = B.Id
            LEFT OUTER JOIN Categories C
            ON P.CategoryId = C.Id
            ORDER BY P.CategoryId, P.BrandId";
    }
}
