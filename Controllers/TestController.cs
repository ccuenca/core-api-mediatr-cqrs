using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestMediaTR.Support;

namespace TestMediaTR.Controllers
{
    [Route("api/test")]
    public class TestController : Controller
    {
        private readonly AmqpOptions _amqpOptions;

        public TestController(IOptionsSnapshot<AmqpOptions> options)
        {
            _amqpOptions = options.Value;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            Log.Information("Amqp option {@ConnectionInfo}", _amqpOptions);
            return Ok(_amqpOptions);
        }

        [HttpGet("error")]
        public IActionResult GetError()
        {
            try
            {
                throw new Exception("Mensaje");
            }
            catch (Exception excpt)
            {
                Log.Error(excpt, "Mensaje de error {@Date}", DateTime.Now);
                return StatusCode(500);
            }            
        }


    }
}
