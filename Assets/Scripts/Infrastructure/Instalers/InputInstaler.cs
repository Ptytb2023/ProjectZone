using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class InputInstaler : MonoInstaller
    {
       [SerializeField] private UIInputModul _uIInputModul;
        public override void InstallBindings()
        {
            base.InstallBindings();
        }
    }
}