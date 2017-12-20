namespace LendYourHome.Application.Infrastructure.Extensions
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Data.Models.Enums;
    using Microsoft.AspNetCore.Html;

    public static class EnumExtensions
    {
        public static string LogTypeDisplayName(this AdminLogType item)
        {
            var type = item.GetType();
            var member = type.GetMember(item.ToString());
            DisplayAttribute displayName = (DisplayAttribute)member[0].GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault();

            if (displayName != null)
            {
                return displayName.Name;
            }

            return item.ToString();
        }
    }
}
