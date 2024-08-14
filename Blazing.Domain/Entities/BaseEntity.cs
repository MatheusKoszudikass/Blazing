using System.ComponentModel.DataAnnotations;

namespace Blazing.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get;  set; } = Guid.NewGuid();

        public DateTime DataCreated { get;  set; }

        public DateTime? DataUpdated { get;  set; }

        public DateTime? DataDeleted { get;  set; }
    }
}
