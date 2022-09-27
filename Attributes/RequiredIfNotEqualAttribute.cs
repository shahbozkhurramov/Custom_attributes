using System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public sealed class RequiredIfNotEqualAttribute : RequiredAttribute
{
    private readonly string _comparisonProperty;
    private readonly object _comparisonValue;

    public RequiredIfNotEqualAttribute(string comparisonProperty, object comparisonValue)
    {
        _comparisonProperty = comparisonProperty;
        _comparisonValue = comparisonValue;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
        if (property == null)
            throw new ArgumentException("Property with this name not found");

        var comparisonValue = property.GetValue(validationContext.ObjectInstance, null);

        if (comparisonValue == null)
            return ValidationResult.Success;

        if (!comparisonValue.Equals(_comparisonValue))
            return base.IsValid(value, validationContext);

        return ValidationResult.Success;
    }
}