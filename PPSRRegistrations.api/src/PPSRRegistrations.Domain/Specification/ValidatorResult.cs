namespace PPSRRegistrations.Domain.Specification
{
    public class ValidatorResult
    {
        public List<string> ErrorMessage { get; }
        public List<int?> Code { get; }

        public ValidatorResult()
        {
            ErrorMessage = new List<string>();
            Code = new List<int?>();
        }

        public void AddError(string message, int? code = null)
        {
            ErrorMessage.Add(message);
            Code.Add(code);
        }

        public static implicit operator bool(ValidatorResult v) => v.ErrorMessage.Count == 0;
        public static implicit operator string(ValidatorResult v) => string.Join('\n',v.ErrorMessage);
        public static implicit operator int?(ValidatorResult v) => v.Code.FirstOrDefault(x => x != null);
    }
}
