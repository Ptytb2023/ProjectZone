using System;
using System.Collections;
using UnityEngine;

namespace Services
{
    public interface ICoroutineService : IService
    {
        public Coroutine RunCoroutine(IEnumerator coroutine);
        public void Stop(Coroutine coroutine);
    }
}
