using System.Threading.Tasks;

namespace Services.Save
{
    public interface IJsonFileServiceService : IService
    {
        Task<TModel> LoadAsync<TModel>(string filePath);
        Task SaveAsync<TModel>(TModel model, string filePath);
    }
}
