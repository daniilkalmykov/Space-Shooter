using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class MagazinesCountText : MonoBehaviour
    {
        [SerializeField] private List<Weapon.Weapon> _weapons;

        private TMP_Text _magazinesText;

        private void Awake()
        {
            _magazinesText = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            foreach (var weapon in _weapons)
                weapon.MagazinesCountChanged += OnMagazinesCountChanged;
        }

        private void OnDisable()
        {
            foreach (var weapon in _weapons)
                weapon.MagazinesCountChanged -= OnMagazinesCountChanged;
        }

        private void OnMagazinesCountChanged(int magazinesCount)
        {
            _magazinesText.text = magazinesCount.ToString();
        }
    }
}