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
        // GET: api/ProductDb
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbContext.Products);
        }

        // GET: api/ProductDb/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var data = _dbContext.Products.SingleOrDefault(x => x.ProductId == id);
            if(data==null)
            {
                return NotFound("The given ID does not correspond to any data.");
            }
            return Ok(data);
        }
        
        // POST: api/ProductDb
        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges(true);
            return StatusCode(StatusCodes.Status201Created);
        }
        
        // PUT: api/ProductDb/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Product product)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(id!=product.ProductId)
            {
                return BadRequest("The id of the product to be updated has to be the same as the requested ID");
            }
            try
            {
                _dbContext.Products.Update(product);
                _dbContext.SaveChanges(true);
                return StatusCode(StatusCodes.Status200OK);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _dbContext.Products.SingleOrDefault(x => x.ProductId == id);
            if(product==null)
            {
                return NotFound("The given ID does not correspond to any data.");
            }
            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges(true);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
