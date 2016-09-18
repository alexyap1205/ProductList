using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InnoDataAccess;

namespace Innovetric.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        [Route("")]
        public HttpResponseMessage Get()
        {
            Product[] products = null;

            IProductsAccessor accessor = new ProductsAccessor();

            try
            {
                products = accessor.GetProducts().ToArray();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
                var message = "Unable to retrieve product list";
                HttpError error = new HttpError(message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, error);
            }

            return Request.CreateResponse(HttpStatusCode.OK, products);
        }

        //// GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}