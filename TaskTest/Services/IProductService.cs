using TaskTest.API.Models;

namespace TaskTest.API.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductListItemDto>> GetAllAsync(ProductFilterParams filters);
        Task<ProductResponseDto?> GetByIdAsync(int id);
        Task<ProductResponseDto> CreateAsync(CreateProductRequest request);
        Task<bool> UpdateAsync(int id, UpdateProductRequest request);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<ProductResponseDto>> CreateBulkAsync(IEnumerable<CreateProductRequest> requests);
    }
}