using System.Net;

namespace Assignment_API.Models.Dto
{
    public class APIResponseDto
    {

        public APIResponseDto()
        {
            ErrorMessages = new List<string>();
        }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; }

        public object Result { get; set; }
    }
}
