using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections;
using System.Net;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;

namespace MilGlorian.Common.Shared;

public class APIResponse<T> : BaseResponse<T>
{
    public APIResponse(HttpStatusCode status = HttpStatusCode.OK, string message = "Request executed successfully", string state = "Success")
    {
        ResponseCode = status;
        Message = message;
        State = state;
    }
    public APIResponse(T _payload, HttpStatusCode status = HttpStatusCode.OK, string message = "Request executed successfully", string state = "Success")
    {
        Payload = _payload;
        ResponseCode = status;
        Message = message;
        State = state;
    }

    public APIResponse(string message) : base(message)
    {
    }

    public APIResponse(string message, string state) : base(message, state)
    {
    }

    public APIResponse(string message, T payload) : base(message, payload)
    {
    }
    public APIResponse(string message, string state, T payload) : base(message, state, payload)
    {
    }



    [JsonIgnore]
    public HttpStatusCode ResponseCode { get; set; } = HttpStatusCode.OK;

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public IDictionary ErrorDetails { get; set; }

    public ActionResult ToActionResult(bool passParam = true)
    {
        switch (ResponseCode)
        {
            case HttpStatusCode.OK:
                return passParam ? new OkObjectResult(this) : new OkResult();

            case HttpStatusCode.Unauthorized:
                return passParam ? new UnauthorizedObjectResult(this) : new UnauthorizedResult();

            case HttpStatusCode.NotFound:
                return passParam ? new NotFoundObjectResult(this) : new NotFoundResult();

            default:
                return passParam ? new BadRequestObjectResult(this) : new BadRequestResult();
        }
    }
}
