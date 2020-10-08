using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers
{
    //[Produces("application/json")]
    [Route("api/Customers")]
    public class CustomersController : Controller
    {
        static List<Customer> _customers = new List<Customer>()
        {
            new Customer(){Id=0,Name="Ashmita",Email="ashmita.mukherjee@gmail.com",Phone="9999999999"},
            new Customer(){Id=1,Name="Ranjana",Email="nytah.sircar@gmail.com",Phone="9831791939"}
        };
        /// <summary>
        /// Get all cutomers.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_customers);
        }
        /// <summary>
        /// Add a customer object.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddCustomer([FromBody]Customer customer)
        {
            if(ModelState.IsValid)
            {
                _customers.Add(customer);
                return StatusCode(StatusCodes.Status201Created);
            }
            return BadRequest(ModelState);
        }
    }
}