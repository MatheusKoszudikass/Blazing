using System.ComponentModel.DataAnnotations;

namespace Blazing.Domain.Entities
{
    #region Entity revision.
    /// <summary>
    /// Entity responsible for the product review made by the user.
    /// </summary>
    public class Revision
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? User { get; set; }
        public string? Comment { get; set; }
        public DateTime Date { get; set; }
    }
    #endregion
}
