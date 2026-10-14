
using System.ComponentModel.DataAnnotations;

namespace LapShop.Validation
{
    public class AllowedSortByAttribute : ValidationAttribute
    {
        private readonly string[] _allowedValues;

        public AllowedSortByAttribute(params string[] allowedValues)
        {
            _allowedValues = allowedValues;
            ErrorMessage =
        $"SortBy must be one of: {string.Join(", ", allowedValues)}.";
        }

        public override bool IsValid(object? value)
        {
            if (value is null)
                return true;

            if (value is not string sortBy)
                return false;

            return _allowedValues.Any(x =>
                string.Equals(
                    x,
                    sortBy,
                    StringComparison.OrdinalIgnoreCase));
        }
    }
}