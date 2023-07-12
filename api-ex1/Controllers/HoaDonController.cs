using api_ex1.Entities;
using api_ex1.IServices;
using api_ex1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_ex1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private readonly IHoaDonServices _hoaDonServices;
        public HoaDonController()
        {
            _hoaDonServices = new HoaDonServices();
        }
        [HttpPost("addHoaDon")]
        public IActionResult AddHoaDon([FromBody] HoaDon hd)
        {
            var ret = _hoaDonServices.ThemHoaDon(hd);
            if(ret == Constant.ErrorMesssage.ThanhCong)
            {
                return Ok("Them thanh cong");
            }
            else
                return BadRequest("Them that bai");
        }
    }
}
