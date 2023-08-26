using Dapper;
using DapperCRUD.DataAccess;
using DapperCRUD.DTOs.ProductDto;
using DapperCRUD.Models;

namespace DapperCRUD.Business;

public class ProductService : IProductService
{
    private readonly Context _context;

    public ProductService(Context context)
    {
        _context = context;
    }

    public async Task<List<GetProductDto>> GetAllProductAsync()
    {
        string query = "select * from DapperProduct";
        using (var connection = _context.CreateConnection())
        {
            var values = await connection.QueryAsync<GetProductDto>(query);
            return values.ToList();
        }
    }
    
    public async Task<GetProductDto> GetProductById(int id)
    {
        string query = "select * from DapperProduct where ProductId=@ProductId";
        var parameters = new DynamicParameters();
        parameters.Add("@ProductId", id);
        using (var connection = _context.CreateConnection())
        {
            var value = await connection.QueryFirstOrDefaultAsync<GetProductDto>(query, parameters);
            return value;
        }
    }

    public async void InsertProduct(CreateProductDto product)
    {
        string query =
            "insert into DapperProduct (Title, Price, City, District) values (@Title, @Price, @City, @District)";
        var parameters = new DynamicParameters();
        parameters.Add("@Title", product.Title);
        parameters.Add("@Price", product.Price);
        parameters.Add("@City", product.City);
        parameters.Add("@District", product.District);
        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(query, parameters);
        }
    }

    public async void UpdateProduct(UpdateProductDto product)
    {
        string query = "Update DapperProduct Set Title=@title,Price=@price,City=@city ,District=@district  where ProductId=@productId";
        var parameters = new DynamicParameters();
        parameters.Add("@productId", product.ProductId);
        parameters.Add("@title", product.Title);
        parameters.Add("@price", product.Price);
        parameters.Add("@city", product.City);
        parameters.Add("@district", product.District);
        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(query, parameters);
        }
    }
    
    public async void DeleteProduct(int id)
    {
        string query = "Delete From DapperProduct Where ProductId=@productId";
        var parameters = new DynamicParameters();
        parameters.Add("@productId",id);
        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(query, parameters);
        }
    }
}