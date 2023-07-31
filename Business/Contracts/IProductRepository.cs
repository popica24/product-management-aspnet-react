using Business.Domain;
using Business.Services;


namespace Business.Contracts
{
    public interface IProductRepository
    {
        Page<Product> GetAll(int offset, int limit);
        Product Get(int id);
        Page<Product> Filter(ProductSearchParameters searchParameters,int offset,int limit);
        List<string> LoadCategories(int id);
        bool Post(Product product);
        bool Delete(int id);
        bool Put(Product product,int id);
    }
}
