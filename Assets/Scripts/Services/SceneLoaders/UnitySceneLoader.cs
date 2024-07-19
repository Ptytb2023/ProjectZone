using Extensions;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

using Scene = DataPersistence.Scene;

namespace Services.SceneLoaders
{
    public class UnitySceneLoader : IServiceSceneLoader
    {
        public async Task LoadAsync(Scene scene) => 
            await SceneManager.LoadSceneAsync(scene.Name, scene.Mode);

        public async Task UnLoadAsync(Scene scene) => 
            await SceneManager.UnloadSceneAsync(scene.Name);
    }
}
