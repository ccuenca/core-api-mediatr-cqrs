using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
            return Ok(_amqpOptions);
        }
    }
}
