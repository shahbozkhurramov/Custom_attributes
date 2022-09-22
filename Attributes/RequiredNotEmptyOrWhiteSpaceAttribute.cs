using System.ComponentModel.DataAnnotations;

namespace AttributesWebApi.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class RequiredNotEmptyOrWhiteSpaceAttribute : RequiredAttribute
{
    public override bool IsValid(object value)
    {
        if (value is string stringValue)
        {
            return !string.IsNullOrWhiteSpace(stringValue);
        }

        return base.IsValid(value);
    }
}
