using DataPersistence;
using Inventarys.Data;
using Inventarys.Model;
using Player.EquipmentInventores.Data;
using Player.EquipmentInventores.Model;
using Player.EquipmentInventores.Slot;
using Services.Save;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Infrastructure.FSMGame
{
    public class GameStatePreserver : IState
    {
        private ISaveLoadService _saveLoadService;
        private DiContainer _container;

        public GameStatePreserver(ISaveLoadService saveLoadService, DiContainer container)
        {
            _saveLoadService = saveLoadService;
            _container = container;
        }

        public async void Enter()
        {
            InventoryData inventoryData = CreateInventorySlotData();
            List<InventoryEquipmentSlotData> inventoryEquipmentSlotDatas = CreateEquipmentSlot();

            PlayerPorgress playerPorgress = new PlayerPorgress(inventoryData, inventoryEquipmentSlotDatas);

            await _saveLoadService.Save(playerPorgress);
        }

        private InventoryData CreateInventorySlotData()
        {
            var inventory = _container.Resolve<IInventory>();

            int size = inventory.Size;

          var  inventorySlotDatas = inventory.InventorySlots.Select(slot =>
             new InventorySlotData(slot.ItemId.GetValue(), slot.Amount.GetValue())).ToList();

            return new InventoryData(size, inventorySlotDatas);
        }

        private List<InventoryEquipmentSlotData> CreateEquipmentSlot()
        {
            var inventoryEquipment = _container.Resolve<IInventoryEquipment>();
            IInventoryEquipmentSlot[] slots = inventoryEquipment.EquipmentsSlots;

            var inventoryEquipmentSlotDatas = slots.Select(slot =>
         new InventoryEquipmentSlotData(slot.ItemID, slot.Type)).ToList();

            return inventoryEquipmentSlotDatas;
        }

        public void Exit()
        {
        }
    }
}
