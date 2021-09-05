using System;

namespace AgilityCont.DataAccess
{
    public class FormaPagamento
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public Guid? Uer { get; set; }
        public DateTime? Der { get; set; }
    }
}
