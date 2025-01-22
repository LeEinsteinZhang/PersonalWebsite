using Microsoft.AspNetCore.Mvc;

public class ErrorController : Controller
{
    [Route("Error/{code}")]
    public IActionResult GeneralError(int code)
    {
        Response.StatusCode = code;
        ViewData["StatusCode"] = code;

        string errorMessage = code switch
        {
            404 => "Page Not Found",
            500 => "Internal Server Error",
            403 => "Forbidden",
            _ => "An unexpected error occurred"
        };

        ViewData["ErrorMessage"] = errorMessage;

        return View("Error");
    }
}