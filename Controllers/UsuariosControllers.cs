using System.Collections.Generic;
using apitarefas.Models;
using apitarefas.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apitarefas.Controllers
{
    [Authorize]
    [Route("api/usuarios")]
    public class UsuariosControllers : ControllerBase
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
                    _usuario.senha = usuario.senha;
                    _usuario.nome = usuario.nome;
                    _usarioReposity.Update(_usuario);
                    return new NoContentResult();
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