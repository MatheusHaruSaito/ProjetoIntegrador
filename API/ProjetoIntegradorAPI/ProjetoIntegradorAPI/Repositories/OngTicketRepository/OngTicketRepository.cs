using ProjetoIntegradorAPI.Context;
using ProjetoIntegradorAPI.Models;
using ProjetoIntegradorAPI.Repositories.UserRepostory;

namespace ProjetoIntegradorAPI.Repositories.OngTicketRepository
{
    public class OngTicketRepository: BaseRepository<OngTicket>, IOngTicketRepository
    {
        private readonly IUserRepository _userRepository;
        public OngTicketRepository(ApplicationDataContext applicationDataContext, IUserRepository userRepository): base(applicationDataContext)
        {
            _userRepository = userRepository;
        }

        public async Task<OngTicket> AcceptTicket(Guid Id)
        {
            OngTicket ongTicket=  await GetByIdAsync(Id);
            if(ongTicket is null)
            {
                return null;
            }
            ongTicket.Accpeted = true;
            ongTicket.Reviwed = true;


            _userRepository.AddAsync(new User
            {
                Name = ongTicket.Name,
                Email = ongTicket.Email,
                Password = GenerateUserRandomPassword(5),
                Cep = ongTicket.Cep,
                Role = UserRoleEnum.Administrator,
            });
            _applicationDataContext.OngTicket.Update(ongTicket);
            await _applicationDataContext.SaveChangesAsync();
            return ongTicket;
        }
        public async Task<OngTicket> DeclineTicket(Guid Id)
        {
            OngTicket ongTicket = await GetByIdAsync(Id);
            if (ongTicket is null)
            {
                return null;
            }
            ongTicket.Accpeted = false;
            ongTicket.Reviwed = true;
            _applicationDataContext.OngTicket.Update(ongTicket);
            await _applicationDataContext.SaveChangesAsync();
            return ongTicket;
        }
        private string GenerateUserRandomPassword(int length)
        {
            const string validChars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890!@#$%&";
            string password = "";
            Random random = new Random();
            for (int i = 0; i <= length; i++)
            {
                password += validChars[random.Next(0, validChars.Length)];
            }
            return password;
        }

    }

}
