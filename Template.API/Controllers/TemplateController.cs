using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;
using Template.Core.Settings;

namespace Template.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TemplateController : ControllerBase
    {
        private readonly AppSettings _appSettings;

        public TemplateController(IOptions<AppSettings> appSettings)
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
