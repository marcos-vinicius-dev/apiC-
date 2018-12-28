using System;

namespace apitarefas.Models
{
    public class Tarefa
    {
        public int tarefa_id { get; set; }
        public string titulo { get; set; }
        public string descricao  { get; set; }
        public DateTime data_limite { get; set; }
        public string status  { get; set; }
        public DateTime data_cadastro  { get; set; }
        public string visibilidade  { get; set; }
        public int usuario_Id { get; set; }
    }
}