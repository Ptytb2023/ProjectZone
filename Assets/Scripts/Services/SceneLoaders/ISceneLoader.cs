using Data;
using System.Threading.Tasks;

namespace Services.SceneLoaders
{
    public interface ISceneLoader : IService
    {
        public Task LoadAsync(Scene scene);
        public Task UnLoadAsync(Scene scene);
    }
}
