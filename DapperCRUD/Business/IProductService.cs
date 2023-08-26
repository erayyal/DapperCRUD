using DapperCRUD.DTOs.ProductDto;
using DapperCRUD.Models;

namespace DapperCRUD.Business;

public interface IProductService
{
    Task<List<GetProductDto>> GetAllProductAsync();
    void InsertProduct(CreateProductDto product);
    void DeleteProduct(int id);
    void UpdateProduct(UpdateProductDto product);
    Task<GetProductDto> GetProductById(int id);
}