namespace TransnationalLanka.Rms.Mobile.Core.Exceptions
{
    public class ServiceException : Exception
    {

        public ErrorMessage[] Messages { get; set; }

        public ServiceException(ErrorMessage[] messages)
        {
            Messages = messages;
        }

        public ServiceException(ErrorMessage[] messages, Exception exception)
            : base(innerException: exception, message: null)
        {
            Messages = messages;
        }

        public override string ToString()
        {
            return Messages == null ? Message :
                string.Join(" | ", Messages.Select(e => e.Message));
        }

        //public ServiceExceptionMessage[] Messages { get; }

        //public ServiceException(string code, string description)
        //{
        //    Messages = new ServiceExceptionMessage[] { new ServiceExceptionMessage(code, description) };
        //}

        //public ServiceException(ServiceExceptionMessage[] messages)
        //{
        //    this.Messages = messages;
        //}
    }

    //public class ServiceExceptionMessage
    //{
    //    public ServiceExceptionMessage(string code, string description)
    //    {
    //        this.Code = code;
    //        this.Description = description;
    //    }

    //    public string Code { get; private set; }
    //    public string Description { get; private set; }
    //}

    public class ErrorMessage
    {
        public string Code { get; set; }
        public object Meta { get; set; }
        public string Message { get; set; }
    }
}
