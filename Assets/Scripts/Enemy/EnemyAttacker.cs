using Player;
using Sounds;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Animator), typeof(EnemyBiteSoundTurntable))]
    public class EnemyAttacker : MonoBehaviour
    {
        [SerializeField] private float _timeBetweenPunches;
        [SerializeField] private float _damage;

        private Animator _animator;
        private EnemyBiteSoundTurntable _enemyBiteSoundTurntable;
        private float _delay;
        private bool _canAttack = true;
        private bool _isWaiting;
        private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");

        public PlayerHealth PlayerHealth { get; private set; }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _enemyBiteSoundTurntable = GetComponent<EnemyBiteSoundTurntable>();
        }

        private void Update()
        {
            if (PlayerHealth == null)
                _enemyBiteSoundTurntable.StopSounds();
            
            if (_isWaiting)
            {
                _delay += Time.deltaTime;
                
                if (_delay >= _timeBetweenPunches)
                {
                    _canAttack = true;
                    _isWaiting = false;

                    _delay = 0;
                }
            }
        }

        public void Init(PlayerHealth playerHealth)
        {
            PlayerHealth = playerHealth;
        }

        public void Attack()
        {
            if (_canAttack)
            {
                PlayerHealth.TakeDamage(_damage);
                _animator.SetTrigger(IsAttacking);

                _isWaiting = true;
                _canAttack = false;
                _enemyBiteSoundTurntable.PlayBiteAudioClip();
            }
        }
        
        public void IncreaseDamage(int value)
        {
            if (value > 0)
                _damage += value;
        }
    }
}