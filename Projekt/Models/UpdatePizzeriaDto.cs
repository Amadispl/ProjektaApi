using System.ComponentModel.DataAnnotations;

namespace Projekt.Models
{
    public class UpdatePizzeriaDto
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool HasDelivery { get; set; }
    }
}
