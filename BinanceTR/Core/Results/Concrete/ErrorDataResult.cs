namespace BinanceTR.Core.Results.Concrete
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T data, string message, int code) : base(data, false, message, code)
        {
        }

        public ErrorDataResult(T data) : base(data, false)
        {
        }

        public ErrorDataResult(string message) : base(default, false, message, 0)
        {

        }

        public ErrorDataResult() : base(default, false)
        {

        }
    }
}
