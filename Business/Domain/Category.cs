namespace Business.Domain
{
    public class Category
    {
        public int CategoryId { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }

        public int ProductCount { get; set; } = 0;

        public List<int> Products { get; set; }
    }
}
