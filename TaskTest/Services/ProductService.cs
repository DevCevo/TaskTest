using Microsoft.EntityFrameworkCore;
using TaskTest.API.Data;
using TaskTest.API.Entities;
using TaskTest.API.Models;

namespace TaskTest.API.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductListItemDto>> GetAllAsync(ProductFilterParams filters)
        {
            var query = _context.Products.AsQueryable();

            // Filtering Logic
            if (!string.IsNullOrEmpty(filters.Category))
                query = query.Where(p => p.Category.ToLower() == filters.Category.ToLower());

            if (filters.MinPrice.HasValue)
                query = query.Where(p => p.Price >= filters.MinPrice.Value);

            if (filters.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= filters.MaxPrice.Value);

            var products = await query.ToListAsync();

            return products.Select(MapToListItemDto);
        }

        public async Task<ProductResponseDto?> GetByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return product == null ? null : MapToDto(product);
        }

        public async Task<ProductResponseDto> CreateAsync(CreateProductRequest request)
        {
            var product = new Product
            {
                Name = request.Name,
                Category = request.Category,
                Price = request.Price,
                StockQuantity = request.StockQuantity,
                CreatedAt = DateTime.UtcNow
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return MapToDto(product);
        }

        public async Task<bool> UpdateAsync(int id, UpdateProductRequest request)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;


            if (!string.IsNullOrEmpty(request.Name)) product.Name = request.Name;
            if (!string.IsNullOrEmpty(request.Category)) product.Category = request.Category;
            if (request.Price.HasValue) product.Price = request.Price.Value;
            if (request.StockQuantity.HasValue) product.StockQuantity = request.StockQuantity.Value;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        private static ProductResponseDto MapToDto(Product p)
        {
            return new ProductResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                CreatedAt = p.CreatedAt,
            };
        }

        private static ProductListItemDto MapToListItemDto(Product p)
        {
            return new ProductListItemDto
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                CreatedAt = p.CreatedAt,

                InStock = p.StockQuantity > 0 ? true : null
            };
        }

        public async Task<IEnumerable<ProductResponseDto>> CreateBulkAsync(IEnumerable<CreateProductRequest> requests)
        {
            var products = requests.Select(request => new Product
            {
                Name = request.Name,
                Category = request.Category,
                Price = request.Price,
                StockQuantity = request.StockQuantity,
                CreatedAt = DateTime.UtcNow
            }).ToList();

            _context.Products.AddRange(products);
            await _context.SaveChangesAsync();

            return products.Select(MapToDto);
        }
    }
}