using System.Drawing;
using Domain.CatalogModule.Entities.Categories;
using Domain.CatalogModule.Entities.Units;
using SharedKernel.Base;
using SharedKernel.Interfaces;

namespace Domain.CatalogModule.Entities.Products
{
    public class Product : Entity, IAggregateRoot, ICreationTrackable, IUpdateTrackable, ISoftDeletable
    {
        public string Code { get; private set; } = string.Empty;
        public string Name { get; private set; } = string.Empty;
        public long? CategoryId { get; private set; }
        public long UnitId { get; private set; }
        public int MinStock { get; private set; } = 0;
        public string? Note { get; private set; }

        #region Audit Fields
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        #endregion Audit Fields

        #region Navigation Properties
        public Unit Unit { get; private set; } = null!;
        public Category? Category { get; private set; } = null!;
        
        #endregion Navigation Properties

        protected Product() { } // EF cần constructor mặc định
        public Product(
            string code,
            string name,
            long? categoryId,
            long unitId
        )
        {
            Code = code;
            Name = name;
            CategoryId = categoryId;
            UnitId = unitId;
        }

        public void SetMinStock(int minStock)
        {
            MinStock = minStock;
        }

        public void SetNote(string? note)
        {
            Note = note;
        }
    }
}