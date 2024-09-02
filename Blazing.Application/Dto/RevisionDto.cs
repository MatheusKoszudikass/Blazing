using Blazing.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blazing.Application.Dto
{
    #region DTO revision.
    /// <summary>
    /// DTO responsible for the product review made by the user.
    /// </summary>
    public sealed class RevisionDto : BaseEntityDto
    {
        public UserDto? User { get; set; }
        public string? Comment { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    } 
    #endregion
}
