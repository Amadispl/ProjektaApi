namespace Projekt.Models
{
    public class PizzeriaDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool HasDelivery { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Postalcode { get; set; }
        public List<MenuDto> Menu { get; set; }
    }
}
