using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers
{
    [Produces("application/json")]
    [Route("api/Products")]
    public class ProductsController : Controller
    {
        static List<Product> _products = new List<Product>()
        {
            new Product(){ProductId=0,ProductName="GPU",ProductPrice="20000"},
            new Product(){ProductId=1,ProductName="CPU",ProductPrice="30000"}
        };
        /// <summary>
        /// Get all products.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> Get()
        {
            return _products;
        }
        /// <summary>
        /// Post a Product object and add it to the static list.
        /// </summary>
        /// <param name="product"></param>
        [HttpPost]
        public void Post([FromBody]Product product)
        {
            _products.Add(product);
        }
        /// <summary>
        /// Update a Product object in the static List
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="product"></param>
        [HttpPut("{productId}")]
        public void Put(int productId, [FromBody]Product product)
        {
            _products[productId] = product;
        }
        /// <summary>
        /// Delete a Product object from the static List
        /// </summary>
        /// <param name="productId"></param>
        [HttpDelete("{productId}")]
        public void Delete(int productId)
        {
            _products.RemoveAt(productId);
        }

    }
}