using Bramka.Server.Services;
using Bramka.Shared.Interfaces.Services;
using Bramka.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bramka.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogService _logService;

        public LogController( ILogService logService)
        {
            _logService = logService;
        }

        [HttpGet]
        [Route("history")]
        public async Task<IActionResult> GetAllLog()
        {
            try
            {
                var logs = await _logService.GetAllLogsAsync();
                return Ok(logs);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("getLog/{logId}")]
        public async Task<IActionResult> GetLog(int logId)
        {
            try
            {
                var log = await _logService.GeLogByIdAsync(logId);
                if (log == null)
                {
                    return NotFound();
                }
                return Ok(log);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("userHistory/{userId}")]
        public async Task<IActionResult> GetLogByUserId(Guid userId)
        {
            try
            {
                var qrCode = await _logService.GetLogByUserIdAsync(userId);
                if (qrCode == null)
                {
                    return NotFound();
                }
                return Ok(qrCode);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteLog(int logId)
        {
            try
            {
                var success = await _logService.DeleteLogAsync(logId);
                if (success)
                {
                    return Ok();
                }
                return NotFound();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
