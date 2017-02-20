namespace SmartHouse.Web.Models.Response
{
    public class ErrorResponse : ResponseBase
    {
        public ErrorResponse()
        {
            this.Success = false;
        }

        public ErrorResponse(string message)
        {
            this.Message = message;
            this.Success = false;
        }
    }
}