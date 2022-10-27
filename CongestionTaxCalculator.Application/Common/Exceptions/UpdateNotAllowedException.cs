namespace CongestionTaxCalculator.Application.Common.Exceptions
{
    public class UpdateNotAllowedException: Exception
    {
        public UpdateNotAllowedException()
            : base()
        {
        }

        public UpdateNotAllowedException(string message)
            : base(message)
        {
        }

        public UpdateNotAllowedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public UpdateNotAllowedException(string name, object key)
            : base($"Entity \"{name}\" ({key}) not allowed to update.")
        {
        }
    }
}