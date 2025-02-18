
namespace Domain.Entities.Abstractions
{
    public abstract class EntityBaseTracking<Tid> : IEntityBase<Tid>, IDateTracking, ISoftDelete
    {
        public required Tid Id { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset LastModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
