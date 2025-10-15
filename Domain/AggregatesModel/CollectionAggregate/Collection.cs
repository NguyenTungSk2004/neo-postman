using Domain.Common.Extensions;
using Domain.SeedWork;

namespace Domain.AggregatesModel.CollectionAggregate
{
    public class Collection: AuditEntity
    {
        public long WorkspaceId { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string? Description { get; private set; }

        private List<Folder> _folders = new();
        public IReadOnlyCollection<Folder> Folders => _folders.AsReadOnly();

        public Collection(long workspaceId, string name, string? description)
        {
            WorkspaceId = workspaceId;
            Name = name;
            Description = description;
        }

        public void UpdateName(string name)
        {
            Name = name;
            this.MarkUpdated();
        }

        public void UpdateDescription(string? description)
        {
            Description = description;
            this.MarkUpdated();
        }

        public void AddFolder(string name)
        {
            var folder = new Folder(name);
            _folders.Add(folder);
            this.MarkUpdated();
        }
        public void RemoveFolder(long folderId)
        {
            var folder = _folders.FirstOrDefault(x => x.Id == folderId);
            if (folder is null)
                return;

            _folders.Remove(folder);
            this.MarkUpdated();
        }
    }
}