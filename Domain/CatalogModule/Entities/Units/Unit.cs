using SharedKernel.Base;
using SharedKernel.Interfaces;

namespace Domain.CatalogModule.Entities.Units
{
    public class Unit : Entity, IAggregateRoot, ICreationTrackable, ISoftDeletable
    {
        public string Code { get; private set; } = string.Empty;
        public string Name { get; private set; } = string.Empty;

        #region Audit Fields
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        #endregion Audit Fields

        protected Unit() { } // EF cần constructor mặc định
        public Unit(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}