using DataPersistence;
using Inventarys.Data;
using Inventarys.Model;
using Player.EquipmentInventores.Data;
using Player.EquipmentInventores.Model;
using Player.EquipmentInventores.Slot;
using Services.Save;
using System.Collections.Generic;
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

            InventoryData inventoryData = CreatInventorySlotData();

            List<InventoryEquipmentSlotData> inventoryEquipmentSlotDatas = CreatEquipmentSlot();

            PlayerPorgress playerPorgress = new PlayerPorgress(inventoryData, inventoryEquipmentSlotDatas);

            await _saveLoadService.Save(playerPorgress);
        }

        private InventoryData CreatInventorySlotData()
        {
            List<InventorySlotData> inventorySlotDatas;
            int size;

            var inventory = _container.Resolve<IInventory>();

            inventorySlotDatas = new();
            size = inventory.Size;
            foreach (var slot in inventory.InventorySlots)
            {
                InventorySlotData inventorySlotData =
                    new InventorySlotData(slot.ItemId.GetValue(), slot.Amount.GetValue());

                inventorySlotDatas.Add(inventorySlotData);
            }

            return new InventoryData(size, inventorySlotDatas);
        }

        private List<InventoryEquipmentSlotData> CreatEquipmentSlot()
        {
            var inventoryEquipment = _container.Resolve<IInventoryEquipment>();
            IInventoryEquipmentSlot[] slot = inventoryEquipment.EquipmentsSlots;
            List<InventoryEquipmentSlotData> inventoryEquipmentSlotDatas = new();
            foreach (var slotData in inventoryEquipment.EquipmentsSlots)
            {
                var newSlot = new InventoryEquipmentSlotData(slotData.ItemID, slotData.Type);
                inventoryEquipmentSlotDatas.Add(newSlot);
            }

            return inventoryEquipmentSlotDatas;
        }

        public void Exit()
        {
        }
    }
}
