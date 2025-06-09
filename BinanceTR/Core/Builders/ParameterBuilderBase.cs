using System.Globalization;

namespace BinanceTR.Core.Builders;

public abstract class ParameterBuilderBase<T> where T : ParameterBuilderBase<T>
{
    protected abstract void AddParameterInternal(string key, object value);

    public T AddParameter(string key, object? value)
    {
        if (value != null)
        {
            AddParameterInternal(key, value);
        }
        return (T)this;
    }

    public T AddParameterIf(bool condition, string key, object? value)
    {
        if (condition && value != null)
        {
            AddParameterInternal(key, value);
        }
        return (T)this;
    }

    public T AddOptionalParameter<TValue>(string key, TValue? value) where TValue : struct
    {
        if (value.HasValue)
        {
            AddParameterInternal(key, value.Value);
        }
        return (T)this;
    }

    public T AddOptionalParameter(string key, string? value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            AddParameterInternal(key, value);
        }
        return (T)this;
    }

    public T AddDecimal(string key, decimal? value)
    {
        if (value.HasValue)
        {
            AddParameterInternal(key, value.Value.ToString(CultureInfo.InvariantCulture));
        }
        return (T)this;
    }

    public T AddEnum<TEnum>(string key, TEnum? value) where TEnum : struct, Enum
    {
        if (value.HasValue)
        {
            AddParameterInternal(key, (int)(object)value.Value);
        }
        return (T)this;
    }

    public T AddDateTimeOffset(string key, DateTimeOffset? value)
    {
        if (value.HasValue)
        {
            AddParameterInternal(key, value.Value.ToUnixTimeMilliseconds());
        }
        return (T)this;
    }

    public T AddString(string key, string? value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            AddParameterInternal(key, value);
        }
        return (T)this;
    }
}
