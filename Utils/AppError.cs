namespace Notes.Utils
{
    public class AppError
    {
        public string Message { get; set; }
        public int Status { get; set; }

        public AppError() { }

        public AppError(string message)
        {
            Message = message;
            Status = StatusCodes.Status400BadRequest;
        }

        public AppError(string message, int status)
        {
            Message = message;
            Status = status;
        }
    }
}