namespace SmartHouse.Web.Models.Response
{
    public class SuccessResponse : ResponseBase
    {
        public SuccessResponse()
        {
            this.Success = true;
        }

        public SuccessResponse(string message)
        {
            this.Message = message;
            this.Success = true;
        }
    }
}