using System.ComponentModel.DataAnnotations;

namespace WebApiDemo.Models
{
    /// <summary>
    /// Model class for a sample Product
    /// </summary>
    public class Product
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage ="The product name is mandatory, duh-uh!"),StringLength(20,ErrorMessage = "Please enter a string which is lesser than 20 characters")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "The product price is mandatory, duh-uh!")]
        public string ProductPrice { get; set; }
    }
}
