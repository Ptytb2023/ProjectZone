using Shooting.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Shooting
{
    public class ForTestShooting : MonoBehaviour
    {
        [SerializeField] private WeaponSystem _weponSystem;
        [SerializeField] private GunProjectile _gunProjectile;


        private void Start() => 
            _weponSystem.EquipWeapon(_gunProjectile);
    }
}
