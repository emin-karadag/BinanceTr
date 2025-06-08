namespace BinanceTR.Core.Results.Abstract
{
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
        long Code { get; }
    }
}
