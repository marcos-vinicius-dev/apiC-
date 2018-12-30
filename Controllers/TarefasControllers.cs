using System;
using System.Collections.Generic;
using apitarefas.Models;
using apitarefas.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apitarefas.Controllers
{
    
    [Route("api/tarefas")]
    [Authorize()]
    public class TarefasControllers : ControllerBase
    {
        private readonly ITarefaRepository _tarefaReposity;

        public TarefasControllers(ITarefaRepository usuRep)
        {
            _tarefaReposity = usuRep;
        }

        [HttpGet]
        public IEnumerable<Tarefa> GetAll()
        {
           return _tarefaReposity.GetAll();
        }


        [HttpGet("{id}", Name="GetTarefa")]
        public IActionResult GetById(int id)
        {
            var tarefa = _tarefaReposity.Find(id);
            if(tarefa==null)
            {
                return NotFound();
            }else{
                return new ObjectResult(tarefa);
            }       
        }

        [HttpPost]
        public IActionResult Create([FromBody] Tarefa tarefa)
        {
            if(tarefa==null)
            {
                return BadRequest();
            }
            else
            {
                tarefa.data_cadastro = DateTime.Now;
                _tarefaReposity.Add(tarefa);
                return CreatedAtRoute("GetTarefa", new {id=tarefa.tarefa_id}, tarefa);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Tarefa tarefa)
        {
            if(tarefa==null || tarefa.tarefa_id != id)
            {
                return BadRequest();
            }
            else
            {
                var _tarefa = _tarefaReposity.Find(id);
                if(tarefa==null)
                {
                    return NotFound();
                }else{
                    _tarefa.titulo = tarefa.titulo;
                    _tarefa.descricao = tarefa.descricao;
                    _tarefa.data_limite = tarefa.data_limite;
                    _tarefa.status = tarefa.status;
                    _tarefa.data_cadastro = tarefa.data_cadastro;
                    _tarefa.visibilidade = tarefa.visibilidade;
                    _tarefa.usuario_Id = tarefa.usuario_Id;
                    _tarefaReposity.Update(_tarefa);
                    return new NoContentResult();//void
                }
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var tarefa = _tarefaReposity.Find(id);
            if(tarefa==null){
                return NotFound();
            }else{
                _tarefaReposity.Remove(id);
                return new NoContentResult();
            }
        }

    }
}