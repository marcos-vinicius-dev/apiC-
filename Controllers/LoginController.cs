using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using apitarefas.Models;
using apitarefas.Repositorio;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace apitarefas.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioRepository _usarioReposity;
       
        public LoginController(IUsuarioRepository usuRep){
             _usarioReposity = usuRep;
        }

        [HttpPost]
        public IActionResult Token(LoginModel model)
        {
            var usuario = _usarioReposity.Find(model.login, model.senha);
        
            if(usuario!=null)
                {
                    //token (header + payload >> direitos(claims) + signature)
                    
                    var direitos = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, model.login),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())//valor unico
                    };

                    var chave = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("apitarefas-authentication-valid"));
                    
                    var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        issuer: "apitarefas",
                        audience: "Postman",
                        claims: direitos,
                        signingCredentials: credenciais,
                        expires: DateTime.Now.AddMinutes(30)
                    );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                    return Ok(tokenString);
                }
                return Unauthorized(); //401
        }
    }
}