using Domain.Abstraction;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class Permission : BaseEntity<int>, ISoftDeletable
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ICollection<Group> Groups { get; set; } = new List<Group>();
    }
}
