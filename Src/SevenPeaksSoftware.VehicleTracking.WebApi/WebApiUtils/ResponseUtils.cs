using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SevenPeaksSoftware.VehicleTracking.Application.Enums;
using SevenPeaksSoftware.VehicleTracking.Application.ViewModels;

namespace SevenPeaksSoftware.VehicleTracking.WebApi.WebApiUtils
{

    // extension method to return ResponseDto in an standard format  by correct HTTP status code.  
    public static class ResponseUtils
    {
        //Return Response by correct HTTP status code
        public static IActionResult ResponseHandler<T>(this ResponseDto<T> response)
        {
            ResponseEnums.ErrorEnum statusCode;

            if (response.Success)
            {
                statusCode = ResponseEnums.ErrorEnum.Ok;
            }
            else
            {
                statusCode = (ResponseEnums.ErrorEnum)response.Error.StatusCode;
            }

            switch (statusCode)
            {
                case ResponseEnums.ErrorEnum.Ok:
                    return new OkObjectResult(response);


                case ResponseEnums.ErrorEnum.NotFound:
                    return new NotFoundObjectResult(response);

                case ResponseEnums.ErrorEnum.Forbidden:
                    return new ForbidResult();

                case ResponseEnums.ErrorEnum.BadRequest:
                    return new BadRequestObjectResult(response);

                case ResponseEnums.ErrorEnum.NoContent:
                    return new NoContentResult();

                case ResponseEnums.ErrorEnum.Conflict:
                    return new ConflictObjectResult(response);

            }
            return new BadRequestObjectResult("Undefined Error Response");
        }

        //Normalize Bad Request that generate by dot .net to our standard format. 
        public static ResponseDto<object> BadRequestErrorHandler(this ModelStateDictionary ModelState)
        {
            var error = new StringBuilder();
            var i = 1;
            foreach (var modelState in ModelState)
            {
                var key = !string.IsNullOrEmpty(modelState.Key) ? modelState.Key + ":":"";
                foreach (var modelError in modelState.Value.Errors)
                {
                    error = error.Append( i + "-" + key + modelError.ErrorMessage + Environment.NewLine);
                    i++;
                }
            }

            return (new ResponseDto<object>()
            {
                Success = false,
                Error = new ErrorDto()
                {
                    StatusCode = (int) ResponseEnums.ErrorEnum.BadRequest,
                    StatusType = ResponseEnums.ErrorEnum.BadRequest.ToString(),
                    ErrorMessage = error.ToString()
                }
            });
        }
    }
}
