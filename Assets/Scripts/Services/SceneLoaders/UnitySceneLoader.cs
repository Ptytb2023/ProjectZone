using Extensions;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

using Scene = Data.Scene;

namespace Services.SceneLoaders
{
    public class UnitySceneLoader : ISceneLoader
    {
        public async Task LoadAsync(Scene scene) => 
            await SceneManager.LoadSceneAsync(scene.Name, scene.Mode);

        public async Task UnLoadAsync(Scene scene) => 
            await SceneManager.UnloadSceneAsync(scene.Name);
    }
}
