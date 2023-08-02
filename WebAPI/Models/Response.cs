namespace WebAPI.Models
{
    public class Response<T>
    {
        public List<T> Data { get; private set; }
        public bool HasMore { get;private set; }
        public int TotalRecords { get; set; }
        public Response( List<T> data, bool hasMore, int totalRecords)
        {
            Data = data;
            HasMore = hasMore;
            TotalRecords = totalRecords;
        }
    }
}
