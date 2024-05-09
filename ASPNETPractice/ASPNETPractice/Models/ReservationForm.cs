using System.ComponentModel.DataAnnotations;

namespace ASPNETPractice.Models
{
    public class ReservationForm
    {
        [Required]
        [Display(Name ="Křestní jméno")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Přijmení")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Telefonní číslo")]
        public string TelNumber { get; set; }
        [Required]
        [Display(Name = "Počet osob")]
        public int PersonCount { get; set; }
        [Required]
        [Display(Name = "Čas od")]
        public DateTime TimeFrom { get; set; }
        [Required]
        [Display(Name = "Čas do")]
        public DateTime TimeTill { get; set; }
    }
}
