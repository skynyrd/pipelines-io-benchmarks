using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "test";
        }

        [HttpPost("streamreader")]
        public async Task<string> UseStreamReaderAsync()
        {
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                return await reader.ReadToEndAsync();
            }
        }

        [HttpPost("pipelines")]
        public async Task<string> UsePipelines()
        {
            var test = await Request.BodyReader.ReadAsync(HttpContext.RequestAborted);
            var str = Encoding.UTF8.GetString(test.Buffer.FirstSpan);
            Request.BodyReader.AdvanceTo(test.Buffer.Start, test.Buffer.End);

            return str;
        }
    }
}
