using System;
using System.Collections;
using UnityEngine;

namespace Services.Coroutines
{
    public class CoroutineService : MonoBehaviour, ICoroutineService
    {
        public Coroutine RunCoroutine(IEnumerator coroutine)
        {
            if (coroutine is null)
                throw new NullReferenceException($"{nameof(coroutine)} it should not be null");

            return StartCoroutine(coroutine);
        }

        public void Stop(Coroutine coroutine)
        {
            if (coroutine is null)
                throw new NullReferenceException($"{nameof(coroutine)} it should not be null");

            StopCoroutine(coroutine);
        }
    }
}
