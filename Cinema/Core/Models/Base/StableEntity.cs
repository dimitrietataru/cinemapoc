using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Models.Base
{
    public class StableEntity<T> : IEntity<T>, IDeletable
        where T : struct
    {
        [Key]
        public T Id { get; set; }
        public DateTime CreatedAt { get; protected set; }
        public Guid CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

        public StableEntity() => CreatedAt = DateTime.UtcNow;
    }
}
