
namespace Domain.Entities.Abstractions
{
    public interface IDateTracking
    {
        DateTimeOffset CreatedDate { get; set; }
        DateTimeOffset LastModifiedDate { get; set; }
        public string? UpdatedBy { get; set; } 

    }
}
