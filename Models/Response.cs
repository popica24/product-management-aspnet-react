namespace WebAPI.Models
{
    public class Response<T>
    {
        public int TotalRecords { get; private set; }
        public List<T> Data { get; private set; }

        public Response(int records, List<T> data)
        {
            TotalRecords = records;
            Data = data;
        }
    }
}
