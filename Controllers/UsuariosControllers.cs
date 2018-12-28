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

        [HttpPost]
        public IActionResult Create([FromBody] Usuario usuario)
        {
            if(usuario==null)
            {
                return BadRequest();
            }
            else
            {
                _usarioReposity.Add(usuario);
                return CreatedAtRoute("GetUsuario", new {id=usuario.Id}, usuario);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Usuario usuario)
        {
            if(usuario==null || usuario.Id != id)
            {
                return BadRequest();
            }
            else
            {
                var _usuario = _usarioReposity.Find(id);
                if(usuario==null)
                {
                    return NotFound();
                }else{
                    _usuario.Nome = usuario.Nome;
                    _usuario.Funcao = usuario.Funcao;
                    _usuario.Telefone = usuario.Telefone;
                    _usuario.Ramal = usuario.Ramal;
                    _usarioReposity.Update(_usuario);
                    return new NoContentResult();//void
                }
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var usuario = _usarioReposity.Find(id);
            if(usuario==null){
                return NotFound();
            }else{
                _usarioReposity.Remove(id);
                return new NoContentResult();
            }
        }

    }
}