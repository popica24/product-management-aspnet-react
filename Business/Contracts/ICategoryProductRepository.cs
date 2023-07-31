using Business.Domain;

namespace Business.Contracts
{
    public interface ICategoryProductRepository
    {
        bool DeleteCategory (int id);
        bool DeleteProduct (int id);
        bool Delete(CategoryProduct item);
        bool Post(CategoryProduct item);
    }
}
