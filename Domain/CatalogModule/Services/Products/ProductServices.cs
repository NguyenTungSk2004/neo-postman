using Domain.CatalogModule.Entities.Products;
using SharedKernel.Exceptions;
using SharedKernel.Interfaces;

namespace Domain.CatalogModule.Services.Products
{
    public class ProductService
    {
        private readonly IReadRepository<Product> _repository;

        public ProductService(IReadRepository<Product> repository)
        {
            _repository = repository;
        }
        public async Task EnsureCodeIsUnique(string code, long? excludeId = null)
        {
            if (await _repository.AnyAsync(ProductSpecification.ByCode(code, excludeId)))
            {
                throw new BusinessRuleViolationException("Product with the same code already exists.");
            }
        }
    }
}