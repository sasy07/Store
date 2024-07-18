namespace Domain.Entities.Base;

public interface ICommands
{
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public bool IsDelete { get; set; }
    public string Summary { get; set; }
}