
namespace Business.Domain
{
    public class Page<T>
    {
        public IEnumerable<T>? Data { get; private set; }
        public int Limit { get; private set; }
        public int Offset { get; private set; }
        public int Count { get;private set; }
        public bool hasMoreItems { get; set; }
        public bool isLastPage { get; set; }

        public Page(int offset,int limit,int count, IEnumerable<T> data)
        {
            Data = data; Limit = limit; Offset = offset;Count = count;
            hasMoreItems = offset + limit < count;
            isLastPage = offset + limit >= count;
        }
    }
}
