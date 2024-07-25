using DataPersistence;
using System.Threading.Tasks;

namespace Services.Save
{

    public class SaveLoadService : ISaveLoadService
    {
        private readonly FilePathSo _pathToPlayerProggress;
        private readonly IJsonFileServiceService _service;

        public SaveLoadService(FilePathSo pathToPlayerProggress,
            IJsonFileServiceService service)
        {
            _pathToPlayerProggress = pathToPlayerProggress;
            _service = service;

            _pathToPlayerProggress.EnsureCreated();
        }

        public async Task<PlayerPorgress> Load() =>
            await _service.LoadAsync<PlayerPorgress>(_pathToPlayerProggress.Value);

        public async Task Save(PlayerPorgress playerPorgress) =>
            await _service.SaveAsync(playerPorgress, _pathToPlayerProggress.Value);
    }
}
