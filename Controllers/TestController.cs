using Microsoft.AspNetCore.Mvc;

namespace TranslateHelperBot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : Controller
    {
        [HttpGet]
        public string Index()
        {
            return "test controller";
        }
    }
}