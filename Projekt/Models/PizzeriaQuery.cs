namespace Projekt.Models
{
    public class PizzeriaQuery
    {
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
        public string? searchPhrase { get; set; }
    }
}
