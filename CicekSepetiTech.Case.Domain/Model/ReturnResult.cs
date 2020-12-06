namespace CicekSepetiTech.Case.Domain.Model
{
    public class ReturnResult
    {
        public ReturnStatus Status { get; set; }
        public string Message { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorCategory { get; set; }
    }
}