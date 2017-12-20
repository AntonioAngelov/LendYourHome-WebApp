namespace LendYourHome.Tests.Mocks.Services.Admin
{
    using LendYourHome.Services.AdminServices;
    using Moq;

    public class UserAdminServiceMock
    {
        public static Mock<IUserAdminService> New
            => new Mock<IUserAdminService>();
    }
}
