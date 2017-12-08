namespace LendYourHome.Application.Infrastructure.Attributes.ModelValidation
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DateValidationAttribute : ValidationAttribute
    {
        public DateValidationAttribute()
        {
            this.ErrorMessage = "Date must be in the future.";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            var date = (DateTime) value;

            return date > DateTime.UtcNow;
        }
    }
}
