using Business.Domain;

namespace WebAPI.Models
{
    public class Page<T>
    {

        public Page(PaginatedList<T> entity)
        {
            PageNumber = entity.PageNumber;
            PageSize = entity.PageSize;
            TotalPages = entity.TotalPages; 
            TotalRecords = entity.TotalRecords;
            Data = entity.Data;
            HasMoreItems = entity.HasMoreItems;
            HasPreviousPage = entity.HasPreviousPage;
            IsLastPage = entity.IsLastPage;
            HasMoreItems = !entity.HasMoreItems;
        }

        public int PageNumber { get; private set; } = 1;
       
        public int PageSize { get; private set; } = 100;
       
        public int TotalPages { get; private set; }
       
        public int TotalRecords { get; private set; }
       
        public IEnumerable<T> Data { get; private set; }
      
        public bool HasNextPage { get; private set; }
      
        public bool HasPreviousPage { get; private set; }
       
        public bool IsLastPage { get; private set; }
      
        public bool HasMoreItems { get; private set; }

    }
}
