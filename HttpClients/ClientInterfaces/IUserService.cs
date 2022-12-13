using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IUserService
{
    Task<Bruger> Create(Bruger bruger);

    Task resetBruger(string depotID);
}