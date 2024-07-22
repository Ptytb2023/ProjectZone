using DataPersistence;
using Inventarys.Data;
using Inventarys.Model;
using ItemSystem.Items.Equipments;
using Player.EquipmentInventores.Data;
using Player.EquipmentInventores.Model;
using Services;
using Services.Save;
using System.Collections.Generic;
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
        private void Construct(ISaveLoadService saveService, DiContainer diContainer,IItemService itemService)
        {
            _diContainer = diContainer;
            _saveService = saveService;
        }

        private void Awake() => 
            DontDestroyOnLoad(this);

        public async override Task InitializeAsync()
        {
            PlayerPorgress playerProgress = await _saveService.Load();

            if (playerProgress is null)
            {
                BindInventory(_inventoryDataMainStart);
                BindEquipmentInventory(_slotDataEquipment);
            }
            else
            {
                BindInventory(playerProgress.MainInvetory);
                BindEquipmentInventory(playerProgress.InventoryEquipments.ToArray());
            }
        }

        private void BindInventory(InventoryData data)
        {
            IInventory inventory = new Inventory(data);

            _diContainer.BindInterfacesAndSelfTo<Inventory>().FromInstance(inventory).AsSingle().NonLazy();
        }

        private void BindEquipmentInventory(InventoryEquipmentSlotData[] _slotDataEquipment)
        {

            var items = new List<ItemEquipment>(maxEquipmentSlot);

            foreach (var slotData in _slotDataEquipment)
            {
                var item = (ItemEquipment)_itemService.GetItem(slotData.IdItem);

                items.Add(item);
            }

            IInventoryEquipment inventory = new InventoryEquipment(items);

            _diContainer.BindInterfacesAndSelfTo<InventoryEquipment>().FromInstance(inventory).AsSingle().NonLazy();
        }
    }
}
