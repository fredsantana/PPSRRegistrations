namespace PPSRRegistrations.Application.ViewModels
{
    public class ResponseViewModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }

        protected ResponseViewModel()
        {
            Message = string.Empty;
            Result = null;
        }

        public static ResponseViewModel Ok(string message = "")
        {
            return new ResponseViewModel
            {
                Success = true,
                Message = message
            };
        }
        
        public static ResponseViewModel Ok(object result)
        {
            return new ResponseViewModel
            {
                Success = true,
                Result = result
            };
        }
        
        public static ResponseViewModel Error(string message)
        {
            return new ResponseViewModel
            {
                Success = false,
                Message = message
            };
        }
    }
}
