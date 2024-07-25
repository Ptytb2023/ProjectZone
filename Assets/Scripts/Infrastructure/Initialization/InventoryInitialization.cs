using DataPersistence;
using Inventarys.Data;
using Inventarys.Model;
using Player.EquipmentInventores.Data;
using Player.EquipmentInventores.Model;
using Services;
using Services.Save;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Infrastructure.Initialization
{
    public class InventoryInitialization : AsyncInitialization
    {
        private const int maxEquipmentSlot = 9;

        [SerializeField] private InventoryData _inventoryDataMainStart;
        [SerializeField] private InventoryEquipmentSlotData[] _slotDataEquipment = new InventoryEquipmentSlotData[maxEquipmentSlot];

        private DiContainer _diContainer;
        private ISaveLoadService _saveService;
        private IItemService _itemService;

        [Inject]
        private void Construct(ISaveLoadService saveService, DiContainer diContainer, IItemService itemService)
        {
            _diContainer = diContainer;
            _saveService = saveService;
            _itemService = itemService;
        }

        private void Awake() =>
            DontDestroyOnLoad(this);

        private async void OnEnable()
        {
           await InitializeAsync();
        }

        public async override Task InitializeAsync()
        {

            PlayerPorgress playerProgress = await _saveService.Load();

            if (playerProgress is null)
            {
                playerProgress = new PlayerPorgress(_inventoryDataMainStart, _slotDataEquipment.ToList());
                await _saveService.Save(playerProgress);
            }

            BindInventory(playerProgress.MainInvetory);
            BindEquipmentInventory(playerProgress.InventoryEquipments.ToArray());
        }

        private void BindInventory(InventoryData data)
        {
            IInventory inventory = new Inventory(data);

            _diContainer.BindInterfacesAndSelfTo<Inventory>().FromInstance(inventory).AsSingle().NonLazy();
        }

        private void BindEquipmentInventory(InventoryEquipmentSlotData[] _slotDataEquipment)
        {
            IInventoryEquipment inventory = new InventoryEquipment(_slotDataEquipment.ToList());

            _diContainer.BindInterfacesAndSelfTo<InventoryEquipment>().FromInstance(inventory).AsSingle().NonLazy();
        }
    }
}
