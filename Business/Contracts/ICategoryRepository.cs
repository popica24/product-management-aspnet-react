using Business.Domain;
using Business.Services;

namespace Business.Contracts
{
    public interface ICategoryRepository
    {
        Page<Category> GetAll(int offset, int limit);
        Category Get(int id);
        Page<Category> Filter(CategorySearchParameters parameters,int offset,int limit);
        Page<Product> LoadProducts(int categoryId,int offset,int limit);
        bool Post(Category category);
        bool Delete(int id);
        bool Put(Category item, int id);
    }
}
