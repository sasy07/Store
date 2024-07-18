namespace Domain.Entities.Base;

public class BaseAuditableEntity : BaseEntity
{
    public DateTime Created { get; set; }
    public int CreateBy { get; set; }
    public DateTime? LastModified { get; set; }
    public int? ModifiedBy { get; set; }
}