using Domain.Common.Extensions;
using Domain.SeedWork;

namespace Domain.AggregatesModel.CollectionAggregate
{
    public class Folder: AuditEntity
    {
        public string Name { get; private set; }
        public long? ParentFolderId { get; private set; }

        private List<Folder> _subFolders = new();
        public IReadOnlyCollection<Folder> SubFolders => _subFolders.AsReadOnly();

        public Folder(string name)
        {
            Name = name;
        }
        public void UpdateName(string name)
        {
            Name = name;
            this.MarkUpdated();
        }
        public void AddSubFolder(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Subfolder name cannot be empty.", nameof(name));
            var folder = new Folder(name) { ParentFolderId = this.Id };
            _subFolders.Add(folder);
            this.MarkUpdated();
        }
    }
}