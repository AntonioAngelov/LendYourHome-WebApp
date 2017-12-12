namespace LendYourHome.Application.Infrastructure.Authorization
{
    using Microsoft.AspNetCore.Authorization;

    public class RoleRequirement : IAuthorizationRequirement
    {
        public RoleRequirement(string role)
        {
            this.Role = role;
        }

        public string Role { get; set; }
    }
}
