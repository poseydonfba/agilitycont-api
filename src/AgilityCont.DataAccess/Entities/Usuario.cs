using System;

namespace AgilityCont.DataAccess
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public Guid? Uer { get; set; }
        public DateTime? Der { get; set; }
        public string Chave { get; set; }
        public int Ativo { get; set; }
        public int Tipo { get; set; }
        public string Foto { get; set; }
    }
}
