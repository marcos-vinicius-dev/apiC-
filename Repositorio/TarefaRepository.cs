using System.Collections.Generic;
using System.Linq;
using apitarefas.Models;

namespace apitarefas.Repositorio
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly BancoContext db;
        public TarefaRepository(BancoContext dbContext)
        {
            db = dbContext;
        }
        public void Add(Tarefa tarefa)
        {
            db.Tarefas.Add(tarefa);
            db.SaveChanges();
        }

        public Tarefa Find(long id)
        {
            return db.Tarefas.FirstOrDefault(t => t.tarefa_id == id);
        }

        public IEnumerable<Tarefa> GetAll()
        {
            return db.Tarefas.ToList();
        }

        public void Remove(long id)
        {
            var entity = db.Tarefas.First(t=> t.tarefa_id == id);
            db.Tarefas.Remove(entity);
            db.SaveChanges();
        }

        public void Update(Tarefa tarefa)
        {
            db.Tarefas.Update(tarefa);
            db.SaveChanges();
        }

    }
}