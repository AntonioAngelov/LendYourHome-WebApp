namespace LendYourHome.Services.AdminServices.AdminServiceModels
{
    using System.ComponentModel.DataAnnotations;

    public class UserBaseAdminServiceModel
    {
        public string Id { get; set; }

        public string Address { get; set; }

        public virtual string Email { get; set; }
        
        public virtual string UserName { get; set; }
    }
}
