namespace SeaBattle2Lib.Experiments
{
    public struct Result<T>
    {
        public readonly T Value;
        public readonly string? Error;

        public Result(T value)
        {
            Value = value;
            Error = null;
        }

        public Result(string error)
        {
            Error = error;
            Value = default(T);
        }

        public bool HasValue => Error == null;
        
        public static implicit operator bool (Result<T> result)
        {
            return result.HasValue;
        }
        
    }
}