using InvoiceManager.App.PresentationDto;
using InvoiceManager.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InvoiceManager.App.PresentationMappers
{
    public static class AdviseMessageMapper
    {
        public static IActionResult Map(Exception error)
        {
            var statusCode = MapHttpStatus(error);
            string message = statusCode is null ? "Not Mapped Exception Occurred" : error.Message;
           
            return new ObjectResult(new AdviseMessgeRs(message))
            {
                StatusCode = statusCode
            };

        }

        public static int? MapHttpStatus(Exception error)
        {
            if (error.GetType() == typeof(IdNotFoundException)) return (int)HttpStatusCode.NotFound;
            if (error.GetType() == typeof(DatabaseException)) return (int)HttpStatusCode.InternalServerError;
            if (error.GetType() == typeof(BadRequestException)) return (int)HttpStatusCode.BadRequest;
            if (error.GetType() == typeof(DupplicateKeyException)) return (int)HttpStatusCode.BadRequest;
            if (error.GetType() == typeof(NotFoundException)) return (int)HttpStatusCode.BadRequest;
            return null;
        }
    }
}
