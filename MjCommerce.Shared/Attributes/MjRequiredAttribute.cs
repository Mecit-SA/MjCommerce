using System.ComponentModel.DataAnnotations;

namespace MjCommerce.Shared.Attributes
{
    class MjRequiredAttribute : RequiredAttribute
    {
        public MjRequiredAttribute()
        {
            ErrorMessage = "Required";
        }

        public override bool IsValid(object value)
        {
            bool parsedToInt = int.TryParse(value as string, out int intValue);

            return parsedToInt? intValue > 0 : base.IsValid(value);
        }
    }
}