using Ardalis.Specification;
using Domain.CatalogModule.Entities.Products;

namespace Domain.CatalogModule.Services.Products
{
    public class ProductSpecification : Specification<Product>
    {
        private ProductSpecification(string? code, long? excludeId = null)
        {
            if (!string.IsNullOrEmpty(code)) Query.Where(x => x.Code == code);
            if (excludeId.HasValue) Query.Where(x => x.Id != excludeId.Value);  
        }

        public static ProductSpecification ByCode(string code, long? excludeId = null) => new(code, excludeId);
    }
}