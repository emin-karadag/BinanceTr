namespace BinanceTR.Core.Builders;

public class RequestBuilder : ParameterBuilderBase<RequestBuilder>
{
    private readonly Dictionary<string, object> _parameters;

    public RequestBuilder()
    {
        _parameters = [];
    }

    protected override void AddParameterInternal(string key, object value)
    {
        _parameters[key] = value;
    }

    public Dictionary<string, object> Build() => _parameters;

    public static RequestBuilder Create() => new();
}
