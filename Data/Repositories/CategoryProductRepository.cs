using Business.Contracts;
using Business.Domain;
using Dapper;


namespace Data.Repositories
{
    public class CategoryProductRepository : ICategoryProductRepository
    {
        private readonly IConnectionString _connectionString;

        public CategoryProductRepository(IConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Delete(CategoryProduct item)
        {
            string sql = "delete from CategoryProduct where CategoryId = @CategoryId AND ProductId = @ProductId; select @@ROWCOUNT";

            using var db = new SqlDataContext(_connectionString);

            int? response = db.Connection.QueryFirstOrDefault<int?>(sql,item);

            return (response.HasValue && response.Value == 1);
        }

        public bool DeleteCategory(int id)
        {
            string sql = "delete from CategoryProduct where CategoryId = @id; select @@ROWCOUNT";

            using var db = new SqlDataContext(_connectionString);

            int? response = db.Connection.QueryFirstOrDefault<int?>(sql, new { id });

            return(response.HasValue && response.Value > 0);
        }

        public bool DeleteProduct(int id)
        {
            string sql = "delete from CategoryProduct where ProductId = @id; select @@ROWCOUNT";

            using var db = new SqlDataContext(_connectionString);

            int? response = db.Connection.QueryFirstOrDefault<int?>(sql, new { id });

            return (response.HasValue && response.Value > 0);
        }

        public bool Post(CategoryProduct item)
        {
            string sql = @"insert into CategoryProduct values (@CategoryId, @ProductId);
                        select @@ROWCOUNT;";

            using var db = new SqlDataContext(_connectionString);

            int? id = db.Connection.QueryFirstOrDefault<int?>(sql, item);
            return (id > 0 && id.HasValue);
        }
    }
}
