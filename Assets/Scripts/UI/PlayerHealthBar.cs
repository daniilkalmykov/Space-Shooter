using Player;
using UnityEngine;

namespace UI
{
    public class PlayerHealthBar : Bar
    {
        [SerializeField] private PlayerHealth _playerHealth;
        
        private void Start()
        {
            SetStartValues(_playerHealth.MaxHealth, _playerHealth.MaxHealth);
        }

        private void OnEnable()
        {
            _playerHealth.Changed += OnValueChanged;
        }

        private void OnDisable()
        {
            _playerHealth.Changed -= OnValueChanged;
        }
    }
}
