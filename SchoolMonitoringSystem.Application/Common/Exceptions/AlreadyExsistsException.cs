namespace SchoolMonitoringSystem.Application.Common.Exceptions
{
    public  class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string message) : base(message)
        {
            
        }
    }
}
