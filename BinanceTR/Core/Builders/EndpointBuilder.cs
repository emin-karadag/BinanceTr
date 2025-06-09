using System.Text;

namespace BinanceTR.Core.Builders;

public class EndpointBuilder(string baseEndpoint) : ParameterBuilderBase<EndpointBuilder>
{
    private readonly StringBuilder _queryBuilder = new();
    private bool _hasFirstParameter = false;

    protected override void AddParameterInternal(string key, object value)
    {
        if (!_hasFirstParameter)
        {
            _queryBuilder.Append('?');
            _hasFirstParameter = true;
        }
        else
        {
            _queryBuilder.Append('&');
        }

        _queryBuilder.Append($"{key}={value}");
    }

    public string Build()
    {
        return baseEndpoint + _queryBuilder.ToString();
    }

    public static EndpointBuilder Create(string baseEndpoint) => new(baseEndpoint);
}
