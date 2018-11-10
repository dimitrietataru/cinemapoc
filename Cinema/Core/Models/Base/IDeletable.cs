namespace Core.Models.Base
{
    public interface IDeletable
    {
        bool IsDeleted { get; set; }
    }
}
