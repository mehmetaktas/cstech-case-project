namespace CicekSepetiTech.Case.Domain.Model
{
    public class ReturnModel<T>
    {
        public ReturnModel()
        {
            Result = new ReturnResult();
            Result.Status = ReturnStatus.Error;
        }

        public T Data { get; set; }
        public ReturnResult Result { get; set; }
    }
}