using System;
using System.Collections;
using Sounds;
using UnityEngine;

namespace Weapon
{
    [RequireComponent(typeof(Animator))]
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private Sprite _icon;
        [SerializeField] private Magazine _newMagazine;
        [SerializeField] private Magazine _magazine;
        [SerializeField] private Bullet _bullet;
        [SerializeField] private Transform _shootingPosition;
        [SerializeField] private WeaponSoundsGenerator _weaponSoundsGenerator;
        [SerializeField] private AudioClip _reload;
        [SerializeField] private AudioClip _shooting;
        [SerializeField] private ParticleSystem _shootingParticleSystem;
        [SerializeField] private int _maxBulletsCount;
        [SerializeField] private int _magazinesCount;
        [SerializeField] private float _reloadTime;
        [SerializeField] private float _timeBetweenShots;
        [SerializeField] private float _newMagazineAliveTime;
        [SerializeField] private float _newMagazineStartAliveTime;

        public event Action Reloaded;  
        public event Action Shot;
        public event Action<int, int> BulletsCountChanged;
        public event Action<int> MagazinesCountChanged;
 
        private readonly int _isShooting = Animator.StringToHash("IsShooting");
        
        private Animator _animator;
        private int _currentBulletsCount;
        private bool _isShot;
        private bool _isReloadCoroutineStarted;
        
        public Sprite Icon => _icon;
        public bool IsReloading { get; private set; }

        private void Awake()
        {
            _animator = GetComponent<Animator>();

            if (_bullet.StartDamage < _bullet.Damage)
                _bullet.SetDamage();
        }

        private void OnEnable()
        {
            BulletsCountChanged?.Invoke(_currentBulletsCount, _maxBulletsCount);
            MagazinesCountChanged?.Invoke(_magazinesCount);
        }

        private void Start()
        {
            _currentBulletsCount = _maxBulletsCount;
            BulletsCountChanged?.Invoke(_currentBulletsCount, _maxBulletsCount);
            
            _newMagazine.gameObject.SetActive(false);
        }
        
        public void Shoot()
        {
            if (_currentBulletsCount <= _maxBulletsCount && _currentBulletsCount > 0 && _isShot == false && IsReloading == false)
            {
                _weaponSoundsGenerator.PlayAudioClip(_shooting);
                Shot?.Invoke();
                
                _animator.SetTrigger(_isShooting);
                _isShot = true;
                
                Instantiate(_shootingParticleSystem, _shootingPosition.position, Quaternion.identity);
                
                var bullet = Instantiate(_bullet, _shootingPosition, false);
                bullet.Init(_player);
                
                _currentBulletsCount--;
                BulletsCountChanged?.Invoke(_currentBulletsCount, _maxBulletsCount);
                
                if (_currentBulletsCount <= 0)
                    StartCoroutine(Reload());
                
                StartCoroutine(WaitTimeBetweenShots());
            }
        }

        public void IncreaseMaxBulletsCount(int value)
        {
            _maxBulletsCount += value;
        }

        public void AddMagazinesCount(int count)
        {
            _magazinesCount += count;
        }

        public void StartMagazinesCountChangedAction()
        {
            MagazinesCountChanged?.Invoke(_magazinesCount);
        }

        public void StartBulletsCountChangedAction()
        {
            BulletsCountChanged?.Invoke(_currentBulletsCount, _maxBulletsCount);
        }
        
        public IEnumerator Reload()
        {
            if(_isReloadCoroutineStarted == false && _currentBulletsCount < _maxBulletsCount && _magazinesCount > 0)
            {
                _weaponSoundsGenerator.PlayAudioClip(_reload);
                
                StartCoroutine(AnimateMagazine());
                
                Reloaded?.Invoke();
                
                _isReloadCoroutineStarted = true;
                
                IsReloading = true;

                yield return new WaitForSeconds(_reloadTime);

                _magazinesCount--;
                MagazinesCountChanged?.Invoke(_magazinesCount);
                
                _currentBulletsCount = _maxBulletsCount;
                IsReloading = false;
                
                BulletsCountChanged?.Invoke(_currentBulletsCount, _maxBulletsCount);
                
                _isReloadCoroutineStarted = false;
            }
        }
        
        private IEnumerator AnimateMagazine()
        {
            yield return new WaitForSeconds(_newMagazineStartAliveTime);
            _magazine.gameObject.SetActive(false);
            _newMagazine.gameObject.SetActive(true);
            
            yield return new WaitForSeconds(_newMagazineAliveTime);
            _newMagazine.gameObject.SetActive(false);
            _magazine.gameObject.SetActive(true);
        }

        private IEnumerator WaitTimeBetweenShots()
        {
            yield return new WaitForSeconds(_timeBetweenShots);

            _isShot = false;
        }
    }
}