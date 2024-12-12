using MilGlorian.Domain.Entities.Common;

namespace MilGlorian.Domain.Entities;

public class Biography : BaseEntity
{
    public string Icon { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }
    public Guid CompanyId { get; set; }
    public Company Company { get; set; }
}
