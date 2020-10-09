using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Data;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers
{
    [Produces("application/json")]
    [Route("api/ProductDb")]
    public class ProductDbController : Controller
    {
        ProductDbContext _dbContext;
        public ProductDbController(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// Get by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/ProductDb/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var data = _dbContext.Products.SingleOrDefault(x => x.ProductId == id);
            if (data == null)
            {
                return NotFound("The given ID does not correspond to any data.");
            }
            return Ok(data);
        }

        /// <summary>
        /// Get data via Paging.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPage")]
        public IActionResult GetPage(int? pageNumber, int? pageSize)
        {
            var data = from p in _dbContext.Products.OrderBy(p => p.ProductId) select p;
            int currentPageNumber = pageNumber ?? 1;
            int currentpageSize = pageSize ?? 3;
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Page number or page size cannot be lesser than 1.");
            }
            //Implementing the Skip and Take algorithm.
            var pagedData = data.Skip(currentpageSize*(currentPageNumber - 1)).Take(currentpageSize);
            return Ok(pagedData);
        }

        /// <summary>
        /// Search by Product name.
        /// </summary>
        /// <param name="beginsWith"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("SearchByName")]
        public IActionResult SearchByName(string beginsWith)
        {
            if(beginsWith==null)
            {
                return NotFound("You did not provide any search key. Get your life in an order.");
            }
            var searchResult = _dbContext.Products.Where(x=>x.ProductName.StartsWith(beginsWith)).ToList();
            if (searchResult.Count == 0)
            {
                return NotFound("The search resulted in no results. Therefore the result is as disappointing as your life.");
            }
            return Ok(searchResult);
        }
        /// <summary>
        /// Get all the data. Implemented sorting. Can provide asc or desc values through query parameter.
        /// </summary>
        /// <param name="sort"></param>
        /// <returns></returns>
        // GET: api/ProductDb/5
        [HttpGet]
        public IActionResult Get(string sort)
        {
            IQueryable<Product> productList;
            switch (sort)
            {
                case "desc":
                    productList = _dbContext.Products.OrderByDescending(p => p.ProductPrice);
                    break;
                case "asc":
                    productList = _dbContext.Products.OrderBy(p => p.ProductPrice);
                    break;
                default:
                    productList = _dbContext.Products;
                    break;
            }
            return Ok(productList);
        }
        /// <summary>
        /// Post a product object into the DB.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        // POST: api/ProductDb
        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges(true);
            return StatusCode(StatusCodes.Status201Created);
        }
        /// <summary>
        /// Update a product object into the DB.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        // PUT: api/ProductDb/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != product.ProductId)
            {
                return BadRequest("The id of the product to be updated has to be the same as the requested ID");
            }
            try
            {
                _dbContext.Products.Update(product);
                _dbContext.SaveChanges(true);
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Delete a product object from the DB.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _dbContext.Products.SingleOrDefault(x => x.ProductId == id);
            if (product == null)
            {
                return NotFound("The given ID does not correspond to any data.");
            }
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges(true);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
