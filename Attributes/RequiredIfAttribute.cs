using System.ComponentModel.DataAnnotations;

namespace AttributesWebApi.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class RequiredIfAttribute : ValidationAttribute
{
    private readonly string _propertyName;

    public RequiredIfAttribute(string propertyName)
    {
        _propertyName = propertyName;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        bool isRequired = (bool)validationContext.ObjectType.GetProperty(_propertyName)
                                                            .GetValue(validationContext.ObjectInstance, null);
        if (isRequired)
        {
            if (value == null)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            else if (value is string)
            {
                if (string.IsNullOrWhiteSpace(value as string))
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
        }

        return ValidationResult.Success;
    }
}