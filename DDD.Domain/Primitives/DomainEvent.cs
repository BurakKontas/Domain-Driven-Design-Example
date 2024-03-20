using MediatR;

namespace DDD.Domain.Primitives;

public record DomainEvent(Guid Id) : INotification;
