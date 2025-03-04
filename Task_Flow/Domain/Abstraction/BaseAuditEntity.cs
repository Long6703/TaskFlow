using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Abstraction
{
    public abstract class BaseAuditEntity<TId> where TId : struct
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TId Id { get; set; }
        public DateTime? DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
