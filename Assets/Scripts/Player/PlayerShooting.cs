using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private TakeMagazinesText _takeMagazines;
        [SerializeField] private List<Weapon.Weapon> _weapons;

        public event Action<Sprite> WeaponChanged;
        
        private readonly int _isPistolReloading = Animator.StringToHash("IsPistolReloading");
        private readonly int _isPistolChanged = Animator.StringToHash("IsPistolChanged");
        private readonly int _isPistolShooting = Animator.StringToHash("IsPistolShooting");
        private readonly int _isRiffleChanged = Animator.StringToHash("IsRiffleChanged");
        private readonly int _isRiffleReloading = Animator.StringToHash("IsRiffleReloading");
        private readonly int _isRiffleShooting = Animator.StringToHash("IsRiffleShooting");

        private Animator _animator;
        private MagazinesDropBox _magazinesDropBox;

        public Weapon.Weapon CurrentWeapon { get; private set; }
        public int PistolNumber { get; private set; } = 0;
        public int RiffleNumber { get; private set; } = 1;
        public int CurrentWeaponNumber { get; private set; }
        public bool IsMagazinesDropActivating { get; private set; }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        
        private void OnEnable()
        {
            foreach (var weapon in _weapons)
            {
                weapon.Reloaded += OnReloaded;
                weapon.Shot += OnShot;
            }
        }

        private void OnDisable()
        {
            foreach (var weapon in _weapons)
            {
                weapon.Reloaded -= OnReloaded;
                weapon.Shot -= OnShot;
            }
        }

        private void Start()
        {
            CurrentWeaponNumber = 0;
            CurrentWeapon = _weapons[CurrentWeaponNumber];
            CurrentWeapon.gameObject.SetActive(true);

            for (int i = 1; i < _weapons.Count; i++)
                _weapons[i].gameObject.SetActive(false);
            
            WeaponChanged?.Invoke(CurrentWeapon.Icon);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out MagazinesDropBox magazinesDropBox))
            {
                _takeMagazines.gameObject.SetActive(true);
                IsMagazinesDropActivating = true;

                _magazinesDropBox = magazinesDropBox;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out MagazinesDropBox magazinesDropBox))
            {
                _takeMagazines.gameObject.SetActive(false);
                IsMagazinesDropActivating = false;
            }
        }

        public void TakeDrop()
        {
            _takeMagazines.gameObject.SetActive(false);
            _magazinesDropBox.Unpack();
            Destroy(_magazinesDropBox.gameObject);
            
            if(CurrentWeapon == _magazinesDropBox.Weapon)
                CurrentWeapon.StartMagazinesCountChangedAction();
            
            IsMagazinesDropActivating = false;
        }

        public void ChangeWeapon(int weaponIndex)
        {
            if (weaponIndex >= 0 && weaponIndex < _weapons.Count && CurrentWeapon.IsReloading == false && 
                _weapons[weaponIndex] != CurrentWeapon)
            {
                var previousWeapon = CurrentWeapon;
                previousWeapon.gameObject.SetActive(false);

                CurrentWeaponNumber = weaponIndex;
                CurrentWeapon = _weapons[CurrentWeaponNumber];
                CurrentWeapon.gameObject.SetActive(true);

                _animator.SetTrigger(CurrentWeaponNumber == PistolNumber ? _isPistolChanged : _isRiffleChanged);
                    
                WeaponChanged?.Invoke(CurrentWeapon.Icon);
            }
        }

        public Weapon.Weapon GetRandomWeapon()
        {
            return _weapons[Random.Range(0, _weapons.Count)];
        }

        public Weapon.Weapon GetPistol()
        {
            return _weapons[PistolNumber];
        }

        public Weapon.Weapon GetRiffle()
        {
            return _weapons[RiffleNumber];
        }

        private void OnReloaded()
        {
            _animator.SetTrigger(CurrentWeaponNumber == PistolNumber ? _isPistolReloading : _isRiffleReloading);
        }

        private void OnShot()
        {
            _animator.SetTrigger(CurrentWeaponNumber == PistolNumber ? _isPistolShooting : _isRiffleShooting);
        }
    }
}