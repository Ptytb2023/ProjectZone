using DataPersistence;
using Services;
using Services.Coroutines;
using Services.Input;
using Services.Save;
using Services.SceneLoaders;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Zenject;

namespace Infrastructure.Instalers
{
    public class ServicesInstaller : MonoInstaller
    {
        [SerializeField] private CoroutineService _corutineService;
        [SerializeField] private FilePathSo _pathToPlayerProggress;
        [SerializeField] private PlayerInputView _playerInputView;
        [SerializeField] private ItemDataSo _itemData;

      
        public override void InstallBindings()
        {
            InstalPermisonService();
            InstallSaveLoadService();

            InstallServiceSceneLoader();
            InstallItemService();
            InstallInputService();

            InstalCorutineService();
        }

        private void InstalPermisonService() =>
            Container.BindInterfacesAndSelfTo<PermissionService>().AsSingle().NonLazy();

        private void InstallSaveLoadService()
        {
            IJsonFileServiceService service = new JsonNetFileService();

            Container.BindInterfacesAndSelfTo<SaveLoadService>().
                AsSingle().WithArguments(_pathToPlayerProggress, service).NonLazy();
        }

        private void InstallInputService()
        {
            var playerInputView = Container.InstantiatePrefab(_playerInputView).GetComponent<PlayerInputView>();
            Container.BindInterfacesAndSelfTo<MobileInputService>().AsSingle().WithArguments(playerInputView).NonLazy();

        }

        private void InstallItemService()
        {
            IEnumerable<ItemSystem.IItem> Items = _itemData.Items;
            Container.BindInterfacesAndSelfTo<ItemService>().AsSingle().WithArguments(Items).NonLazy();
        }

        private void InstallServiceSceneLoader() =>
            Container.BindInterfacesAndSelfTo<UnitySceneLoader>().AsSingle().NonLazy();

        private void InstalCorutineService() => 
            Container.BindInterfacesAndSelfTo<CoroutineService>().FromInstance(_corutineService).AsSingle().NonLazy();
    }
}
