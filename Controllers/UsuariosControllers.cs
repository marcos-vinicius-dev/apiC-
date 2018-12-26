using System.Collections.Generic;
using apitarefas.Models;
using apitarefas.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace apitarefas.Controllers
{
    [Route("api/usuarios")]
    public class UsuariosControllers : Controller
    {
        private readonly IUsuarioRepository _usarioReposity;

        public UsuariosControllers(IUsuarioRepository usuRep)
        {
            _usarioReposity = usuRep;
        }

        [HttpGet]
        public IEnumerable<Usuario> GetAll()
        {
           return _usarioReposity.GetAll();
        }

        [HttpGet("{id}", Name="GetUsuario")]
        public IActionResult GetById(int id)
        {
            var usuario = _usarioReposity.Find(id);
            if(usuario==null)
            {
                return NotFound();
            }else{
                return new ObjectResult(usuario);
            }
               
            
        }
    }
}