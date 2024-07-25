using DataPersistence;
using System.Threading.Tasks;

namespace Services.Save
{
    public interface ISaveLoadService
    {
        Task Save(PlayerPorgress playerPorgress);
        Task<PlayerPorgress> Load();
    }
}
