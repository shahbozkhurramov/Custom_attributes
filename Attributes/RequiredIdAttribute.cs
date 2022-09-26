using System.ComponentModel.DataAnnotations;

namespace AttributesWebApi.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public sealed class RequiredIdAttribute : RequiredAttribute
{
    public override bool IsValid(object value)
    {
        if (value == null)
            return false;

        if (value is int)
            return (int)value > 0;

        if (value is long)
            return (long)value > 0;

        if (value is short)
            return (short)value > 0;

        if (value is byte)
            return (byte)value > 0;

        return false;
    }
}