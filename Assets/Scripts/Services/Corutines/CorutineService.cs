﻿using System;
using System.Collections;
using UnityEngine;

namespace Services.Corutines
{
    public class CorutineService : MonoBehaviour, ICorutineService
    {

        public Coroutine StartCorutine(IEnumerator coroutine)
        {
            if (coroutine is null)
                throw new NullReferenceException($"{nameof(coroutine)} it should not be null");

            return StartCoroutine(coroutine);
        }

        public void StopCorutine(Coroutine coroutine)
        {
            if (coroutine is null)
                throw new NullReferenceException($"{nameof(coroutine)} it should not be null");

            StopCoroutine(coroutine);
        }
    }
}