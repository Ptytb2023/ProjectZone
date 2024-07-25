using Shooting.Weapons;
using System.Collections;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ShootinReloadPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textReload;

        private IReloadable _reloadable = new IReloadable.Empty();

        public void SetWeapon(IReloadable reloadable)
        {
            _reloadable.ReloadStart -= OnReloadStart;

            _reloadable = reloadable;
            _reloadable.ReloadStart += OnReloadStart;
            _reloadable.Ammo.SubscribeAndUpdate(OnChangeMagazine);
        }

        private void OnDisable() =>
            _reloadable.ReloadStart -= OnReloadStart;

        private void OnReloadStart(float duration) =>
            StartCoroutine(UpdateReloadTimer(duration));

        private void OnChangeMagazine(int value) => 
            _textReload.text = value.ToString();

        private IEnumerator UpdateReloadTimer(float duration)
        {
            float timeRemaining = duration;

            while (timeRemaining > 0)
            {
                _textReload.text = "Reload: " + Mathf.CeilToInt(timeRemaining).ToString();
                timeRemaining -= Time.deltaTime;
                yield return null;
            }

            _textReload.text = string.Empty;
        }
    }
}