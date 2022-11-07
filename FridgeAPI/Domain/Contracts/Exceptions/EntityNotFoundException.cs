using System;

namespace FridgeAPI.Domain.Contracts.Exceptions
{
    public class EntityNotFoundException: Exception
    {
        public Guid EntityId { get; }


        public EntityNotFoundException(Guid entityId)
            : base()
        {
            EntityId = entityId;
        }

        public EntityNotFoundException(Guid entityId, string? message)
            : base(message)
        {
            EntityId = entityId;
        }
    }
}
