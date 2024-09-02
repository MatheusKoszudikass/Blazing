using System.ComponentModel.DataAnnotations;

namespace Blazing.Domain.Entities
{
    #region Abstract base classe for entites.

    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get;  set; } = Guid.NewGuid();

        public DateTime DataCreated { get; set; } = DateTime.Now;

        public DateTime? DataUpdated { get;  set; }

        public DateTime? DataDeleted { get;  set; }
    }

    #endregion
}
