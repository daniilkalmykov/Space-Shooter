using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : Health
    {
        [SerializeField] private Camera _mainCamera;
        
        public event Action Died;

        private float _blockingDamageTimer;
        private bool _isBlockingDamage;

        private void Update()
        {
            if (_isBlockingDamage && CurrentHealth < MaxHealth)
                Reset();
        }

        public void BlockDamage(float timer)
        {
            _blockingDamageTimer = timer;

            _isBlockingDamage = true;
            
            StartCoroutine(BlockDamage());
        }

        protected override void Die()
        {
            _mainCamera.transform.SetParent(null);
            Died?.Invoke();
            
            gameObject.SetActive(false);
        }

        private IEnumerator BlockDamage()
        {
            yield return new WaitForSeconds(_blockingDamageTimer);
            
            _isBlockingDamage = false;
        }
    }
}
