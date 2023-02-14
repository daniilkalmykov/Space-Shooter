using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class BulletsCountText : MonoBehaviour
    {
        [SerializeField] private List<Weapon.Weapon> _weapons;
        
        private TMP_Text _bulletsCount;

        private void Awake()
        {
            _bulletsCount = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            foreach (var weapon in _weapons)
                weapon.BulletsCountChanged += OnBulletsCountChanged;
        }

        private void OnDisable()
        {
            foreach (var weapon in _weapons)
                weapon.BulletsCountChanged -= OnBulletsCountChanged;
        }

        private void OnBulletsCountChanged(int bulletsCount, int maxBulletsCount)
        {
            _bulletsCount.text = $"{bulletsCount}/{maxBulletsCount}";
        }
    }
}