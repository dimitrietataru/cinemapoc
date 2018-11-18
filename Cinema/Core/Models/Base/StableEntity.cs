using System;

namespace Core.Models.Base
{
    public class StableEntity<T> : IEntity<T>, IStableEntity, IDeletable
        where T : struct
    {
        public StableEntity() => CreatedAt = DateTime.UtcNow;

        public T Id { get; set; }
        public DateTime CreatedAt { get; protected set; }
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
