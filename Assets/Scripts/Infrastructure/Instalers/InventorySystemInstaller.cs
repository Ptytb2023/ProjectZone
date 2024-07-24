using Inventarys;
using Inventarys.View;
using Player.EquipmentInventores;
using Services.Save;
using UnityEngine;
using Zenject;

namespace Infrastructure.Instalers
{
    public class InventorySystemInstaller : MonoInstaller
    {
        [SerializeField] private InventoryController _inventoryController;
        [SerializeField] private InventoryEquipmentView _inventoryEquipment;
        [SerializeField] private InvetoryView _mainInventory;

        private ISaveLoadService _saveLoadService;

        public override void InstallBindings()
        {
            InvetoryView mainInventory;
            InventoryEquipmentView inventoryEquipment;

            InstantiateView(out mainInventory, out inventoryEquipment);

            BindInvetoryView(mainInventory);
            BindEquipmentView(mainInventory, inventoryEquipment);

            Container.BindInterfacesAndSelfTo<InventoryController>().AsSingle().NonLazy();
        }

        private void BindEquipmentView(InvetoryView mainInventory, InventoryEquipmentView inventoryEquipment)
        {
            Container.BindInterfacesAndSelfTo<InventoryEquipmentView>().FromInstance(inventoryEquipment).AsSingle().NonLazy();
            mainInventory.gameObject.SetActive(false);
        }

        private void BindInvetoryView(InvetoryView mainInventory)
        {
            Container.BindInterfacesAndSelfTo<InvetoryView>().FromInstance(mainInventory).AsSingle().NonLazy();
            mainInventory.gameObject.SetActive(false);
        }

        private void InstantiateView(out InvetoryView mainInventory, out InventoryEquipmentView inventoryEquipment)
        {
            mainInventory = Container.InstantiatePrefab(_mainInventory).GetComponent<InvetoryView>();
            inventoryEquipment = Container.InstantiatePrefab(_inventoryEquipment).GetComponent<InventoryEquipmentView>();
            inventoryEquipment.transform.SetParent(mainInventory.transform);
        }
    }
}
