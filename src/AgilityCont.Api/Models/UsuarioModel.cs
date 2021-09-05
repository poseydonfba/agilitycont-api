using System;

namespace AgilityCont.Api.Models
{
    public class UsuarioModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Chave { get; set; }
        public int Ativo { get; set; }
        public int Tipo { get; set; }
        public string Foto { get; set; }
    }
}