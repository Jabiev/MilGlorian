using MilGlorian.Domain.Entities.Common;

namespace MilGlorian.Domain.Entities;

public class Industry : BaseEntity
{
    public string Name { get; set; }
    public ICollection<Company> Companies { get; set; }
}
