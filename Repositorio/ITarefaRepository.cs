using System.Collections.Generic;
using apitarefas.Models;

namespace apitarefas.Repositorio
{
    public interface ITarefaRepository
    {
        void Add (Tarefa tarefa);

         IEnumerable<Tarefa> GetAll();

         Tarefa Find(long id);

         void Remove(long id);

         void Update(Tarefa tarefa);
    }
}