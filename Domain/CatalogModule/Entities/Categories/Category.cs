using SharedKernel.Base;
using SharedKernel.Interfaces;

namespace Domain.CatalogModule.Entities.Categories
{
    public class Category : Entity, IAggregateRoot, ICreationTrackable, IUpdateTrackable, ISoftDeletable
    {
        public string Name { get; private set; } = string.Empty;
        public long? ParentId { get; private set; }

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
        public Category? ParentCategory { get; set; }
        #endregion Navigation Properties

        protected Category() { } // EF cần constructor mặc định
        public Category(string name)
        {
            Name = name;
        }

        public void SetParent(long? parentId)
        {
            ParentId = parentId;
        }
    }
}