using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Helpers;
using Web.Models.View.Login;

namespace Web.Controllers
{
    public class TokenController : ControllerBase
    {
        private readonly JwtHelper _jwtHelper;
        public TokenController(JwtHelper jwtHelper)
        {
            _jwtHelper = jwtHelper;
        }

        [AllowAnonymous]
        [HttpPost("~/signin")]
        public ActionResult<string> SignIn([FromBody] LoginViewModel loginModel)
        {
            if (ValidateUser(loginModel))
            {
                return _jwtHelper.GenerateToken(loginModel.Account);
            }
            else
            {
                return BadRequest();
            }
        }

        private bool ValidateUser(LoginViewModel loginModel)
        {
            return true;
        }

        [Authorize]
        [HttpGet("~/claims")]
        public IActionResult GetClaims()
        {
            return Ok(User.Claims.Select(p => new { p.Type, p.Value }));
            //return JsonResult(User.Claims.Select(p => new { p.Type, p.Value }));
        }

        [Authorize]
        [HttpGet("~/username")]
        public IActionResult GetUserName()
        {
            return Ok(User.Identity.Name);
        }

        [HttpGet("~/jwtid")]
        public IActionResult GetUniqueId()
        {
            var jti = User.Claims.FirstOrDefault(p => p.Type == "jti");
            if(jti == null){
                return Ok("");
            }
            return Ok(jti.Value);
        }
    }
}