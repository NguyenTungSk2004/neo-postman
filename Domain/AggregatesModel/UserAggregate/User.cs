using Domain.SeedWork;
using SharedKernel.SeedWork;

namespace Domain.AggregatesModel.UserAggregate
{
    public class User : Entity, IAggregateRoot, ICreationTrackable, IUpdateTrackable
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string UrlAvatar { get; private set; }
        public DateTime EmailVerifiedAt { get; private set; }
        public bool IsDisabled { get; private set; }

        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }

        public User(string name, string email, string urlAvatar)
        {
            Name = name;
            Email = email;
            UrlAvatar = urlAvatar;
            this.MarkCreated();
        }
        public void Update(string name, string email, string urlAvatar)
        {
            Name = name;
            Email = email;
            UrlAvatar = urlAvatar;
            this.MarkUpdated();
        }
    }
}