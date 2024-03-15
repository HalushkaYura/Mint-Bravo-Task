using Bramka.Server.Interfaces;
using Bramka.Shared.DTOs.QrCodeDTO;
using Bramka.Shared.Interfaces.Services;
using Bramka.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bramka.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QrCodeController : ControllerBase
    {
        private readonly IQrCodeService _qrCodeService;

        public QrCodeController(IQrCodeService qrCodeService)
        {
            _qrCodeService = qrCodeService;
        }

        [HttpGet]
        [Route("last/{userId}")]
        public async Task<IActionResult> GetLastQrCode(Guid userId)
        {
            try
            {
                var qrCode = await _qrCodeService.GetLastQrCodeAsync(userId);
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

        [HttpGet]
        [Route("allQrCode")]
        public async Task<IActionResult> GetAllQrCodes()
        {
            try
            {
                var qrCodes = await _qrCodeService.GetAllQrCodesAsync();
                return Ok(qrCodes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("getQrCode/{qrCodeId}")]
        public async Task<IActionResult> GetQrCodeById(int qrCodeId)
        {
            try
            {
                var qrCode = await _qrCodeService.GetQrCodeByIdAsync(qrCodeId);
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

        [HttpGet]
        [Route("allUserCode/{userId}")]
        public async Task<IActionResult> GetQrCodeByUserId(Guid userId)
        {
            try
            {
                var qrCode = await _qrCodeService.GetQrCodeByUserIdAsync(userId);
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

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateQrCode([FromBody] QrCodeCreateDTO newQrCode)
        {
            try
            {
                var qrCodeId = await _qrCodeService.CreateQrCodeAsync(newQrCode);
                return Ok(qrCodeId);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost]
        [Route("createGuest")]
        public async Task<IActionResult> CreateQrCodeForGuest([FromBody] string code)
        {
            try
            {
                var qrCodeId = await _qrCodeService.CreateQrCodeGuestAsync(code);
                return Ok(qrCodeId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        [Route("delete/{qrCodeId}")]
        public async Task<IActionResult> DeleteQrCode(int qrCodeId)
        {
            try
            {
                var success = await _qrCodeService.DeleteQrCodeAsync(qrCodeId);
                if (!success)
                {
                    return NotFound();
                }

                return Ok();
            }
            catch (Exception ex)             
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
