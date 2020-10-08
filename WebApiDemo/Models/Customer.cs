using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDemo.Models
{
    /// <summary>
    /// Adding Model validation with Data annotations to the Customer Model class.
    /// </summary>
    public class Customer
    {
        public int Id { get; set; }
        [Required,StringLength(20)]
        public string Name { get; set; }
        [Required,RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",ErrorMessage ="Email is not provided in a format that is acceptable. Please re-assess your life.")]
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
