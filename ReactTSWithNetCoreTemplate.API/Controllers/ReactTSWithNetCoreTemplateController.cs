using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;
using ReactTSWithNetCoreTemplate.Core.Settings;

namespace ReactTSWithNetCoreTemplate.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ReactTSWithNetCoreTemplateController : ControllerBase
    {
        private readonly AppSettings _appSettings;

        public ReactTSWithNetCoreTemplateController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"[GET]");
                throw;
            }

        }
    }
}
