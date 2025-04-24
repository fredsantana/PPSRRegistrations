namespace PPSRRegistrations.Domain.Exceptions
{
    public class BusinessException : Exception
    {
        private readonly int? code;

        public int? Code { get { return code; } }

        public BusinessException(string message, int? code = null) : base(message)
        {
            this.code = code;
        }
    }
}
