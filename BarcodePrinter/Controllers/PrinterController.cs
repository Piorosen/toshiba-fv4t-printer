using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Drawing;

namespace BarcodePrinter.Controllers
{
    [ApiController]
    [Route("/Printer")]
    [DisableCors]
    public class PrinterController : ControllerBase
    {
        private readonly ILogger<PrinterController> _logger;

        public PrinterController(ILogger<PrinterController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Get(IFormFile image)
        {
            using var stream = image.OpenReadStream();
#pragma warning disable CA1416 // ?????? ?????? ?????? ????
            using var bitmap = new Bitmap(stream);
#pragma warning restore CA1416 // ?????? ?????? ?????? ????
            var result = Print.Printer.Instance.PrintImage(bitmap);
            var text = $"{{\"result\": {result.ToString().ToLower()}}}";
            if (result)
            {
                return StatusCode(200, text);
            }else
            {
                return StatusCode(500, text);
            }
        }
    }
}