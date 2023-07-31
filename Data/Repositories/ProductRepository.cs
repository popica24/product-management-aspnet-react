using Business.Contracts;
using Business.Domain;
using Business.Services;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly IConnectionString _connectionString;

        public ProductRepository(IConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Delete(int id)
        {
            string sql = "delete from Products where ProductId = @id; select @@ROWCOUNT";

            using var db = new SqlDataContext(_connectionString);

            int response = db.Connection.QueryFirstOrDefault<int>(sql, new { id });

            return response == 1;
        }

        public Page<Product> Filter(ProductSearchParameters searchParameters, int offset, int limit)
        {
            string countSql = CountQuery(searchParameters);

            string sql = FilterQuery(searchParameters);

            using var db = new SqlDataContext(_connectionString);

            int count = db.Connection.ExecuteScalar<int>(countSql,new {searchParameters.categoryId, searchParameters.Name, searchParameters.minQuantity, searchParameters.maxQuantity });
       
            var data = db.Connection.Query<Product>(sql, new {searchParameters.categoryId, searchParameters.Name, offset, limit });

            var paginated = new Page<Product>(offset,limit,count,data);

            return paginated;
        }

        public Product Get(int id)
        {
            string sql = @"select ProductId,Name,Description,Quantity from Products where ProductId = @id";



            using var db = new SqlDataContext(_connectionString);

            Product? item = db.Connection.QueryFirstOrDefault<Product>(sql, new { id });

            return item;
        }

        public Page<Product> GetAll(int offset, int limit)
        {
            string sql = "select ProductId,Name,Description,Quantity from Products order by [ProductId] offset @offset ROWS FETCH NEXT @limit ROW ONLY";

            string countSql = "SELECT COUNT(ProductId) from Products";

            using var db = new SqlDataContext(_connectionString);

            var items = db.Connection.Query<Product>(sql, new { offset, limit });

            var count = db.Connection.ExecuteScalar<int>(countSql);

            var paginated = new Page<Product>(offset,limit,count,items);    

            return paginated;
        }

        public bool Post(Product product)
        {
            string sql = "insert into Products values (@Name, @Description, @Quantity); Select SCOPE_IDENTITY()";

            using var db = new SqlDataContext(_connectionString);

            int? id = db.Connection.QueryFirst<int?>(sql, product);

            return (id > 0);
        }

        public bool Put(Product product, int id)
        {
            string sql = "Update Products set Name = @Name,Description=@Description,Quantity = @Quantity where ProductId = @id; Select @@ROWCOUNT";

            using var db = new SqlDataContext(_connectionString);

            object parameters = new { product.Name, product.Description, product.Quantity, id };

            int? response = db.Connection.QueryFirstOrDefault<int?>(sql, parameters);

            return response == 1;
        }

        public List<string> LoadCategories(int id)
        {
            string sql = @"select C.Name from Categories as C
                            Inner join
                            CategoryProduct as CP on C.CategoryId = CP.CategoryId and CP.ProductId = @id";

            using var db = new SqlDataContext(_connectionString);

            var data = db.Connection.Query<string>(sql, new { id });

            return data.ToList();
        }
        private string FilterQuery(ProductSearchParameters searchParameters)
        {
            var orderBy = searchParameters.orderBy.HasValue ? searchParameters.orderBy.Value.ToString() : "ASC";
            var groupBy = searchParameters.groupBy.HasValue ? searchParameters.groupBy.Value.ToString() : "ProductId";
            switch (searchParameters)
            {
                case { Name: var name } when !string.IsNullOrEmpty(name):
                    return $"SELECT ProductId,Name,Description,Quantity FROM Products WHERE Name LIKE '%' + @Name + '%' order by {orderBy} {groupBy} offset @offset ROWS FETCH NEXT @limit ROW ONLY";
                case { minQuantity: var minQuantity, maxQuantity: var maxQuantity } when minQuantity != 0 && maxQuantity != 0:
                    return $"SELECT ProductId,Name,Description,Quantity from Products WHERE Quantity > @minQuantity AND Quantity < @maxQuantity order by {orderBy} {groupBy} offset @offset ROWS FETCH NEXT @limit ROW ONLY";
                case { minQuantity: var minQuantity } when minQuantity != 0:
                    return $"SELECT ProductId,Name,Description,Quantity from Products WHERE Quantity > @minQuantity order by {orderBy} {groupBy} offset @offset ROWS FETCH NEXT @limit ROW ONLY";
                case { maxQuantity: var maxQuantity } when maxQuantity != 0:
                    return $"SELECT ProductId,Name,Description,Quantity from Products WHERE Quantity < @maxQuantity order by {orderBy} {groupBy} offset @offset ROWS FETCH NEXT @limit ROW ONLY";
                case { categoryId: var categoryId } when categoryId.HasValue && categoryId!= 0:
                    return @$"select Name, Description,Quantity from Products as P
inner join
CategoryProduct as CP on P.ProductId = CP.ProductId and CategoryId = @categoryId
                            order by {orderBy} {groupBy} offset @offset ROWS FETCH NEXT @limit ROW ONLY";

            }
            return $"SELECT ProductId,Name,Description,Quantity FROM Products order by {orderBy} {groupBy} offset @offset ROWS FETCH NEXT @limit ROW ONLY";
        }
        private string CountQuery(ProductSearchParameters searchParameters)
        {
            switch (searchParameters)
            {
                case { Name: var name } when !string.IsNullOrEmpty(name):
                    return "SELECT COUNT(ProductId) from Products WHERE Name LIKE '%' + @Name + '%'";
                case { minQuantity: var minQuantity, maxQuantity: var maxQuantity } when minQuantity != 0 && maxQuantity != 0:
                    return "SELECT COUNT(ProductId) from Products WHERE Quantity > @minQuantity AND Quantity < @maxQuantity";
                case { minQuantity: var minQuantity } when minQuantity != 0:
                    return "SELECT COUNT(ProductId) from Products WHERE Quantity > @minQuantity";
                case { maxQuantity: var maxQuantity } when maxQuantity != 0:
                    return "SELECT COUNT(ProductId) from Products WHERE Quantity < @maxQuantity";
                case { categoryId: var categoryId } when categoryId.HasValue && categoryId != 0:
                    return @$"select COUNT(P.ProductId) from Products as P
inner join
CategoryProduct as CP on P.ProductId = CP.ProductId and CategoryId = @categoryId";
            }
            return "SELECT COUNT(ProductId) from Products";
        }

    }
}
