namespace Projekt.Entities
{
    public class Pizzeria
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool HasDelivery { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        public int AddressId { get; set; }
        public int? CreatedById { get; set; }
        public User CreatedBy { get; set; }
        public virtual Address Address { get; set; }
        public virtual List<Menu> Menu { get; set; }

    }
}
