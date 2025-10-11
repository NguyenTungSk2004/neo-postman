using Application.Common.DomainEvent;
using Domain.SeedWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;
public class AppDbContext : DbContext
{
  private readonly IMediator _mediator;
  public AppDbContext(DbContextOptions<AppDbContext> options, IMediator mediator) : base(options)
  {
    _mediator = mediator;
  }
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    base.OnConfiguring(optionsBuilder);
  }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
  }
  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    var result = await base.SaveChangesAsync(cancellationToken);
    var entities = ChangeTracker.Entries<Entity>()
        .Where(e => e.Entity.DomainEvents.Count > 0)
        .Select(e => e.Entity)
        .ToList();
      
    foreach (var entity in entities)
    {
      var domainEvents = entity.DomainEvents.ToArray();
      entity.ClearDomainEvents();
      foreach (var domainEvent in domainEvents)
      {
          var notification = (INotification)Activator.CreateInstance(
              typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType()), //tạo instance của DomainEventNotification<type>
              domainEvent // truyền domainEvent vào constructor của DomainEventNotification<type>
          )!;

          await _mediator.Publish(notification, cancellationToken);
      }
    }
    return result;
  }
}
