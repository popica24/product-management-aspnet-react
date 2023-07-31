

namespace Business.Services
{
    public class CategorySearchParameters
    {
        public string Name { get; set; }
        public OrderBy? orderBy { get; set; }
        public GroupBy? groupBy { get; set; }
    }
}
