using System.ComponentModel.DataAnnotations;

namespace AttributesWebApi.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public sealed class RequiredIfEqualAttribute : ValidationAttribute
{
    private readonly string _propertyName;
    private readonly object _value;

    public RequiredIfEqualAttribute(string propertyName, object value)
    {
        _propertyName = propertyName;
        _value = value;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var property = validationContext.ObjectType.GetProperty(_propertyName);
        if (property == null)
            return new ValidationResult(string.Format("Unknown property {0}", _propertyName));

        var propertyValue = property.GetValue(validationContext.ObjectInstance, null);
        if (propertyValue == null)
            return new ValidationResult(string.Format("Property {0} is null", _propertyName));

        if (propertyValue.Equals(_value))
        {
            if (value == null)
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            if (value is string && string.IsNullOrWhiteSpace((string)value))
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            if (value is int && (int)value == 0)
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }

        return ValidationResult.Success;
    }
}