// Copyright © CompanyName. All Rights Reserved.

using Configuration.Common.Messages;
using Configuration.Common.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Configuration.Api
{
    public class ValidationProblemDetailsResultHandler : IActionResult
    {
        public async Task ExecuteResultAsync(ActionContext context)
        {
            var modelStateEntries = context.ModelState.Where(e => e.Value.Errors.Count > 0).ToArray();
            var commonResponse = new CommonResponse();

            if (modelStateEntries.Any())
            {
                foreach (var modelStateEntry in modelStateEntries)
                {
                    foreach (var modelStateError in modelStateEntry.Value.Errors)
                    {
                        var customResponseDetails = new CommonResponse.Details()
                        {
                            FieldName = modelStateEntry.Key.Contains(".") ? System.Text.Json.JsonNamingPolicy.CamelCase.ConvertName(modelStateEntry.Key.Split(".")[1].ToString()) : System.Text.Json.JsonNamingPolicy.CamelCase.ConvertName(modelStateEntry.Key.ToString()),
                            Code = AppConstants.ValidationCode,
                            Message = modelStateError.ErrorMessage,
                        };
                        commonResponse.DetailsList.Add(customResponseDetails);
                    }
                }
            }

            commonResponse.Status = AppConstants.Status.Error.ToString();
            commonResponse.TimeStamp = System.DateTime.Now;
            commonResponse.DeveloperMessage = AppConstants.ValidationDeveloperMessage;
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.HttpContext.Response.WriteJson(commonResponse);
        }
    }

}
