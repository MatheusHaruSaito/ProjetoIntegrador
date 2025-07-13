using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace unolink.api.Application.Models.Request.OngTicket
{
    public class CreateOngTicketRequest
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Cep { get; set; }
        public string Cnpj { get; set; }
    }
}