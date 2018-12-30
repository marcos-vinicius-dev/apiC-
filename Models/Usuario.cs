using Microsoft.AspNetCore.Identity;

namespace apitarefas.Models
{
    public class Usuario 
    {
        public int Id { get; set; }
        public string login { get; set; }
        public string senha { get; set; }
        public string nome { get; set; }
    }
}