using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InvoiceManager.App.PresentationMappers
{
    public static class AdviseMessageMapper
    {
        public static IActionResult Map(string error)
        {
            return new ObjectResult(error)
            {
                StatusCode = MapHttpStatus(),
            };

        } 

        public static int MapHttpStatus()
        {
            return (int)HttpStatusCode.InternalServerError;
        }
    }
}
