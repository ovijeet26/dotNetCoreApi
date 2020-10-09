using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Data;

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
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/ProductDb
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/ProductDb/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
