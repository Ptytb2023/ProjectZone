using System.Collections;
using UnityEngine;

namespace Services
{
    public interface ICorutineService : IService
    {
        public Coroutine StartCorutine(IEnumerator coroutine);
        public void StopCorutine(Coroutine coroutine);
    }
}
