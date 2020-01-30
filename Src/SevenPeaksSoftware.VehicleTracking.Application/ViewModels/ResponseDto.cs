namespace SevenPeaksSoftware.VehicleTracking.Application.ViewModels
{
    //Use as an standard Api output in this application
    public class ResponseDto<T>
    {
        //consider the success of Api Call
        public bool Success { get; set; } 

        // return a result when success is true
        public T Result { get; set; }

        //return error detail when success is false
        public ErrorDto Error { get; set; }


        // simplify to return a successful response
        public static ResponseDto<T> SuccessfulResponse(T result)
        {
            return new ResponseDto<T>()
            {
                Result = result,
                Success = true,
                Error = null
            };
        }

        // simplify to return an unsuccessful response
        public static ResponseDto<T> UnsuccessfulResponse(Enums.ResponseEnums.ErrorEnum error, string message = "")
        {
            return new ResponseDto<T>()
            {
                Success = false,
                Error = new ErrorDto()
                {
                    StatusCode = (int)error,
                    StatusType = error.ToString(),
                    ErrorMessage = message
                }
            };
        }
    }
}
