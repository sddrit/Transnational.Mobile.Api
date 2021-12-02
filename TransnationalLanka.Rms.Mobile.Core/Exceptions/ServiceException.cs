namespace TransnationalLanka.Rms.Mobile.Core.Exceptions
{
    public class ServiceException : Exception
    {
        public ServiceExceptionMessage[] Messages { get; }

        public ServiceException(string code, string description)
        {
            Messages = new ServiceExceptionMessage[] { new ServiceExceptionMessage(code, description) };
        }

        public ServiceException(ServiceExceptionMessage[] messages)
        {
            this.Messages = messages;
        }
    }

    public class ServiceExceptionMessage
    {
        public ServiceExceptionMessage(string code, string description)
        {
            this.Code = code;
            this.Description = description;
        }

        public string Code { get; private set; }
        public string Description { get; private set; }
    }
}
