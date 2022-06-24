using System.ComponentModel.DataAnnotations;

namespace Projekt.Models
{
    public class CreatePizzeriaDto
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool HasDelivery { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        public int AddressId { get; set; }
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
        [Required]
        [MaxLength(50)]
        public string Street { get; set; }
        public string Anumber { get; set; }
        public string Postalcode { get; set; }

    }
}
