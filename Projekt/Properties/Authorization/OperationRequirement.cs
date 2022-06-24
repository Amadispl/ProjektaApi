using Microsoft.AspNetCore.Authorization;

namespace Projekt.Properties.Authorization;

public enum OperationRequirementType
{
    Create,
    Update,
    Delete,
    Read
}
public class OperationRequirement : IAuthorizationRequirement

{

    public OperationRequirementType Type { get; set; }
    public OperationRequirement(OperationRequirementType type)
    {
        Type = type;
    }
}
