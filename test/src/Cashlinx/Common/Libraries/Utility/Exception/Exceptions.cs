namespace Common.Libraries.Utility.Exception
{
    /// <summary>
    /// This class should be used to describe when an exceptional case has occured according to the business logic. The use
    /// of this class should provide a clean interface for displaying error messages to the user.
    /// </summary>
    public class BusinessLogicException : System.Exception
    {
        public BusinessLogicException(string Message)
            : base(Message)
        {
        }
    }
}
