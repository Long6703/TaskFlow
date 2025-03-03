
using Domain.Abstraction;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Domain.Entities
{
    public class User : BaseEntity<Guid>, IAuditable, ISoftDeletable
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; }

        [MaxLength(255)]
        public string avatarUrl { get; set; }
        public ICollection<Group> Groups { get; set; } = new List<Group>();
    }
}
