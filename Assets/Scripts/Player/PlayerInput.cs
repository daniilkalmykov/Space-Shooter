using Sounds;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerMovement), typeof(PlayerShooting))]
    [RequireComponent(typeof(PlayerStepsSoundTurntable), typeof(PlayerPause))]
    public class PlayerInput : MonoBehaviour
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";
        private const string Jump = "Jump";
        private const string Fire1 = "Fire1";

        private PlayerMovement _playerMovement;
        private PlayerShooting _playerShooting;
        private PlayerStepsSoundTurntable _playerStepsSoundTurntable;
        private PlayerPause _playerPause;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _playerShooting = GetComponent<PlayerShooting>();
            _playerStepsSoundTurntable = GetComponent<PlayerStepsSoundTurntable>();
            _playerPause = GetComponent<PlayerPause>();
        }

        private void Update()
        {
            if(Time.timeScale == 0)
                return;
            
            float horizontal = Input.GetAxis(Horizontal);
            float vertical = Input.GetAxis(Vertical);
            
            _playerMovement.Move(horizontal, vertical);
            
            if ((horizontal != 0 || vertical != 0) && _playerMovement.OnGround)
                _playerStepsSoundTurntable.PlayStepAudioClip();
            else
                _playerStepsSoundTurntable.StopStepsSound();            
                
            if (Input.GetButtonDown(Jump))
                _playerMovement.Jump();

            Weapon.Weapon weapon;
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                weapon = _playerShooting.CurrentWeapon;
                StartCoroutine(weapon.Reload());
            }

            weapon = _playerShooting.CurrentWeapon;
            
            if (_playerShooting.PistolNumber == _playerShooting.CurrentWeaponNumber) 
            {
                if (Input.GetButtonDown(Fire1))
                    weapon.Shoot();
            }
            else
            {
                if(Input.GetButton(Fire1))
                    weapon.Shoot();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
                _playerShooting.ChangeWeapon(1);
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                _playerShooting.ChangeWeapon(0);

            if (_playerShooting.IsMagazinesDropActivating && Input.GetKeyDown(KeyCode.F))
                _playerShooting.TakeDrop();

            if (Input.GetKeyDown(KeyCode.Escape) && _playerPause.IsPaused == false)
                _playerPause.Pause();                
        }
    }
}
