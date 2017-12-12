namespace LendYourHome.Application.Infrastructure.Authorization
{
    using System.Threading.Tasks;
    using Common.Constants;
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;

    public class RoleHandler: AuthorizationHandler<RoleRequirement>
    {
        private readonly UserManager<User> userManager;

        public RoleHandler(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            //check from cookie first
            if (context.User.IsInRole(ApplicationConstants.HostRole))
            {
                context.Succeed(requirement);
                return;
            }

            var user = await this.userManager.GetUserAsync(context.User);
            

            var userIsHost = user != null && this.userManager.IsInRoleAsync(user, requirement.Role).Result;

            if (userIsHost)
            {
                context.Succeed(requirement);
            }
            
        }
    }
}
