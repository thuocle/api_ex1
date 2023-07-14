using api_ex1.Entities;
using api_ex1.IServices;
using api_ex1.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_ex1.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private readonly IHoaDonServices _hoaDonServices;
        public HoaDonController()
        {
            _hoaDonServices = new HoaDonServices();
        }

        [HttpPost("themHoaDon")]
        public IActionResult ThemHoaDon([FromBody]HoaDonDTO hoaDonDTO)
        {
            var ret = _hoaDonServices.ThemHoaDon(hoaDonDTO.HoaDon);
            if (ret == Constant.ErrorMesssage.ThanhCong)
            {
                var ret2 = _hoaDonServices.ThemChiTietHD(hoaDonDTO.ChiTietHoaDon, hoaDonDTO.HoaDon);
                if (ret2 == Constant.ErrorMesssage.ThanhCong)
                {
                    return Ok("Them thanh cong");
                }
                else
                {
                    return BadRequest("Them chi tiet that bai");
                }
            }
            else
            {
                return BadRequest("Them hoa don that bai");
            }
        }
        [HttpPut("CapNhatHoaDon")]
        public IActionResult SuaHoaDon([FromBody] HoaDonDTO hoaDonDTO)
        {
            var ret = _hoaDonServices.CapNhatHoaDon(hoaDonDTO.HoaDon, hoaDonDTO.ChiTietHoaDon);
            if (ret == Constant.ErrorMesssage.ThanhCong)
            {
                return Ok("Cap nhat thanh cong");
            }
            return BadRequest("Cap nhat hoa don that bai");
        }
        [HttpDelete("XoaHoaDon")]
        public IActionResult XoaHoaDon([FromQuery] int hdID)
        {
            var ret = _hoaDonServices.XoaHoaDon(hdID);
            if (ret == Constant.ErrorMesssage.ThanhCong)
            {
                return Ok("Xoa thanh cong");
            }
            return BadRequest("Xoa hoa don that bai");
        }
        [HttpGet("GetListHoaDon")]
        public IActionResult GetListHD([FromQuery] string? key, [FromQuery] int pageSize = -1, [FromQuery] int pageNumber = 1)
        {
            return Ok(_hoaDonServices.GetListHoaDon(key, pageSize, pageNumber));
        }
    }
}
