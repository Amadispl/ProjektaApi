using System.ComponentModel.DataAnnotations;

namespace Projekt.Models
{
    public class CreateMenuDto
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int PizzeriaId { get; set; }
    }
}
