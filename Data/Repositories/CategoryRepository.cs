using Business.Contracts;
using Business.Domain;
using Business.Services;
using Dapper;
using static System.Net.Mime.MediaTypeNames;


namespace Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IConnectionString _connectionString;
        public CategoryRepository(IConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Delete(int id)
        {
            string sql = "delete from Categories where CategoryId = @id; Select @@ROWCOUNT";

            using var db = new SqlDataContext(_connectionString);

            var result = db.Connection.QueryFirstOrDefault<int>(sql, new { id });

            return result == 1;
        }

        public Category Get(int id)
        {
            string sql = @"select CategoryId, Name, Description from Categories where CategoryId = @id";

            using var db = new SqlDataContext(_connectionString);

            Category? item = db.Connection.QueryFirstOrDefault<Category>(sql, new { id });

            item.ProductCount = CountProducts(id, db);

            return item;
        }

        private int CountProducts(int id, SqlDataContext db)
        {
            string sql = @"select COUNT(CP.ProductId) as ProductCount from Categories as C 
                            Inner join
                            CategoryProduct as CP ON C.CategoryId = CP.CategoryId AND c.CategoryId = @id";
            int count = db.Connection.ExecuteScalar<int>(sql, new { id });
            return count;
        }

        public Page<Category> GetAll(int offset, int limit)
        {
            string sql = "select CategoryId, Name,Description from Categories order by [CategoryId] offset @offset ROWS FETCH NEXT @limit ROW ONLY";
            string countSql = "SELECT COUNT(*) from Categories";
            using var db = new SqlDataContext(_connectionString);

            var data = db.Connection.Query<Category>(sql, new { offset, limit });

            var count = db.Connection.ExecuteScalar<int>(countSql);

            var paginated = new Page<Category>(offset, limit, count, data);

            return paginated;

        }

        public Page<Category> Filter(CategorySearchParameters searchParameters, int offset, int limit)
        {
            string countSql = CountQuery(searchParameters);

            string sql = FilterQuery(searchParameters);

            using var db = new SqlDataContext(_connectionString);

            int count = db.Connection.ExecuteScalar<int>(countSql, new { searchParameters.Name });

            var data = db.Connection.Query<Category>(sql, new { searchParameters.Name, offset, limit });

            var paginated = new Page<Category>(offset, limit, count, data);

            return paginated;

        }

        public bool Post(Category category)
        {
            string sql = @"insert into Categories values (@Name, @Description);
                        select SCOPE_IDENTITY();";

            using var db = new SqlDataContext(_connectionString);

            int? id = db.Connection.QueryFirstOrDefault<int?>(sql, category);
            return (id > 0 && id.HasValue);
        }

        public bool Put(Category item, int id)
        {
            string sql = "Update Categories set Name = @Name,Description=@Description where CategoryId = @id; Select @@ROWCOUNT";



            using var db = new SqlDataContext(_connectionString);
            if (item.Products.Any())
            {
                foreach (var product in item.Products)
                {
                    db.Connection.Execute("INSERT INTO CategoryProduct VALUES (@categoryId,@productId)", new { categoryId = id, productId = product });
                }
            }
            object parameters = new { item.Name, item.Description, id };

            int result = db.Connection.QueryFirstOrDefault<int>(sql, parameters);

            return result == 1;
        }

        public Page<Product> LoadProducts(int categoryId, int offset, int limit)
        {
            string sql = @"select P.ProductId, P.Name, P.Description, P.Quantity 
                   from Products as P
                   Inner join CategoryProduct as CP on P.ProductId = CP.ProductId 
                   and CategoryId = @categoryId 
                   ORDER BY P.ProductId 
                   OFFSET @offset ROWS 
                   FETCH NEXT @limit ROW ONLY";

            string countSql = @"select COUNT(P.ProductId) 
                        from Products as P
                        Inner join CategoryProduct as CP on P.ProductId = CP.ProductId 
                        and CategoryId = @categoryId";

            using var db = new SqlDataContext(_connectionString);

            var data = db.Connection.Query<Product>(sql, new { categoryId, offset, limit });

            var count = db.Connection.ExecuteScalar<int>(countSql, new { categoryId });
            var paginated = new Page<Product>(offset, limit, count, data);
            return paginated;
        }


        private string FilterQuery(CategorySearchParameters searchParameters)
        {
            var orderBy = searchParameters.orderBy.HasValue ? searchParameters.orderBy.Value.ToString() : "ASC";
            var groupBy = searchParameters.groupBy.HasValue ? searchParameters.groupBy.Value.ToString() : "CategoryId";

            switch (searchParameters)
            {
                case { Name: var name } when !string.IsNullOrEmpty(name):
                    return $"SELECT CategoryId,Name,Description from Categories WHERE Name LIKE '%' + @Name + '%' order by {orderBy} {groupBy} offset @offset ROWS FETCH NEXT @limit ROW ONLY";

            }
            return $"SELECT CategoryId,Name,Description from Categories order by {orderBy} {groupBy} offset @offset ROWS FETCH NEXT @limit ROW ONLY";
        }

        private string CountQuery(CategorySearchParameters searchParameters)
        {
            switch (searchParameters)
            {
                case { Name: var name } when !string.IsNullOrEmpty(name):
                    return $"SELECT COUNT(*) from Categories WHERE Name LIKE '%' + @Name + '%'";

            }
            return $"SELECT COUNT(*) from Categories";

        }


    }
}
