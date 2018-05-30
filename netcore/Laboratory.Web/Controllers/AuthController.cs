using Laboratory.NetCore.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Laboratory.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        [HttpPost]
        public IActionResult Token([FromForm] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new ResultData()
                {
                    Code = HttpStatusCode.Unauthorized,
                    Message = ModelState.Values.First(w => w.Errors.Count > 0).Errors[0].ErrorMessage
                });
            }

            if (model.Verification())
            {
                var claims = new[]
                {
                   new Claim(ClaimTypes.Name, model.Name),
                   new Claim(ClaimTypes.Role, "vitrual")
               };
                //sign the token using a secret key.This secret will be shared between your API and anything that needs to check that the token is legit.
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["SecurityKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                //.NET Core’s JwtSecurityToken class takes on the heavy lifting and actually creates the token.
                /**
                    iss: token 使用人
                    sub: token 主题
                    exp: token 过期时间
                    iat: token 创建时间
                    jti: 针对当前 token 的唯一标识
                 * */
                var token = new JwtSecurityToken(
                    issuer: "laboratory.com",
                    audience: "laboratory.com",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                return Json(new ResultData<string>()
                {
                    Code = HttpStatusCode.OK,
                    Message = "ok",
                    Data = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }

            return Json(new ResultData()
            {
                Code = HttpStatusCode.Unauthorized,
                Message = "Could not verify username and password"
            });
        }
    }
}