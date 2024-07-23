using ItemSystem.Items.Equipments;
using UnityEngine;

namespace Player.EquipmentInventores
{
    public interface IEquipmentItemSlot
    {
        EquipmentType Type { get; }

        void ResetIcon();
        void SetIcon(Sprite icon);
    }
}