using ProjetoIntegradorAPI.Models;

namespace ProjetoIntegradorAPI.DTOs.OngTicketDto
{
    public static class OngTicketDtoMapper
    {
        public static OngTicket PostOngTicketDtoToOngTicket(this PostOngTicketDto postOngTicketDto)
        {
            OngTicket ongTicket = new()
            {
                Name = postOngTicketDto.Name,
                Description = postOngTicketDto.Description,
                Email = postOngTicketDto.Email,
                Cnpj = postOngTicketDto.Cnpj,
                Cep = postOngTicketDto.Cep
            };
            return ongTicket;
        }
    }
}
