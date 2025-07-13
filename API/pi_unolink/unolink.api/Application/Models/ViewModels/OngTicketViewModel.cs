using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace unolink.api.Application.Models.ViewModels
{
    public class OngTicketViewModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Cep { get; set; }
        public string Cnpj { get; set; }
        public string CreationDate { get; set; }
        public string ExpirationDate { get; set; }
    }
}