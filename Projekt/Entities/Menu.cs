namespace Projekt.Entities
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int PizzeriaId { get; set; }
        public virtual Pizzeria Pizzeria { get; set; }
    }
}
