using Business.Services;

namespace WebAPI.Models
{
    public class CategoryFilter
    {
        public string? Name { get; set; }
        public string? orderBy { get; set; }
        public string? groupBy { get; set; }

        public CategorySearchParameters ToParameters()
        {
            GroupBy _groupBy = GroupBy.Asc;
            OrderBy _orderBy = OrderBy.Name;

            if (!string.IsNullOrEmpty(groupBy))
            {
                Enum.TryParse(groupBy, out GroupBy _groupByParsed);
                _groupBy = _groupByParsed;

            }
            if (!string.IsNullOrEmpty(orderBy))
            {
                Enum.TryParse(orderBy, out OrderBy _orderByParsed);

                _orderBy = _orderByParsed;

            }

            return new CategorySearchParameters()
            {
                Name = !string.IsNullOrEmpty(Name) ?  Name.Trim() : Name,
                groupBy = _groupBy,
                orderBy = _orderBy,
            };
        }
    }
}
