using System.ComponentModel.DataAnnotations;

namespace MjCommerce.Shared.Attributes
{
    class MjStringLengthAttribute : StringLengthAttribute
    {
        public MjStringLengthAttribute(int maximumLength) : base(maximumLength)
        {
            ErrorMessage = $"Length should not exceed {maximumLength}";
        }

        public MjStringLengthAttribute(int minimumLength, int maximumLength) : base(maximumLength)
        {
            MinimumLength = minimumLength;
            ErrorMessage = $"Length must be between {minimumLength} and {maximumLength}";
        }
    }
}