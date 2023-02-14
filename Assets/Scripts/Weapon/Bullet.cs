using System.Collections;
using UnityEngine;

namespace Weapon
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _bloodParticleSystem;
        [SerializeField] private ParticleSystem _shootParticleSystem;
        [SerializeField] private int _startDamage;
        [SerializeField] private int _damage;
        [SerializeField] private float _speed;
        [SerializeField] private float _lifeTime;

        private Transform _player;

        public int StartDamage => _startDamage;
        public int Damage => _damage;
        
        private void Start()
        {
            StartCoroutine(LiveTime());
            
            transform.SetParent(null);
        }

        private void Update()
        {
            transform.Translate(Vector3.forward * (_speed * Time.deltaTime));
        }

        public void Init(Transform player)
        {
            _player = player;
        }

        public void IncreaseDamage(int damageValue)
        {
            _damage += damageValue;
        }

        public void SetDamage()
        {
            _damage = _startDamage;
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            ParticleSystem effect;
            
            if (collision.gameObject.TryGetComponent(out Enemy.EnemyHealth enemyHealth))
            {
                enemyHealth.TakeDamage(_damage);
                effect = Instantiate(_bloodParticleSystem, transform.position, Quaternion.identity);
            }
            else
            {
                effect = Instantiate(_shootParticleSystem, transform.position, Quaternion.identity);
            }
            
            effect.transform.LookAt(_player);
            effect.transform.SetParent(collision.transform);
            
            Destroy(gameObject);
        }

        private IEnumerator LiveTime()
        {
            yield return new WaitForSeconds(_lifeTime);
            
            Destroy(gameObject);
        }
    }
}