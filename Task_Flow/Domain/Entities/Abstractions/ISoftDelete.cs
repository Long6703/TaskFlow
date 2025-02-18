

namespace Domain.Entities.Abstractions
{
    public interface ISoftDelete
    {
        public bool IsDeleted { get; set; }
    }
}
