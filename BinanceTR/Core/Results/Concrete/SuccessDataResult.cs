namespace BinanceTR.Core.Results.Concrete
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data, string message, long code) : base(data, true, message, code)
        {
        }

        public SuccessDataResult(T data) : base(data, true)
        {
        }

        public SuccessDataResult(string message) : base(default, true, message, 0)
        {

        }

        public SuccessDataResult() : base(default, true)
        {

        }
    }
}
