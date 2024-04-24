using System.ComponentModel.DataAnnotations;

namespace Cviko8ASPNET.Models
{
    public class OrderForm
    {
        //? za typy znamena že to je nepovinna položka ve formuláři
        [Display(Name = "Jméno")]
        [Required]
        public string? Name { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Display(Name = "Počet kusů")]
        [Required(ErrorMessage = "Počet je povinný!")]//Můžeme používat error messages i takhle pokud bychom nechtěli to řešit na Razor Page
        [Range(1, 10)]
        public int? Quantity { get; set; }
    }
}
