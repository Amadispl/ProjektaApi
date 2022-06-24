using Microsoft.AspNetCore.Authorization;
using Projekt.Entities;

using System.Security.Claims;

namespace Projekt.Properties.Authorization
{
    public class OperationHandler : AuthorizationHandler<OperationRequirement, Pizzeria>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationRequirement requirement, Pizzeria pizzeria)
        {
            if (requirement.Type == OperationRequirementType.Read || requirement.Type == OperationRequirementType.Create)
            {
                return Task.CompletedTask;
                context.Succeed(requirement);
            }
            var id = context.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;
            if (pizzeria.CreatedById == int.Parse(id))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
