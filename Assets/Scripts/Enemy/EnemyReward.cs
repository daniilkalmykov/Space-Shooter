using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyReward : MonoBehaviour
    {
        [SerializeField] private MagazinesDropBox _magazinesDropBox;
        [SerializeField] private int _experience;
        [SerializeField] private int _chanceToGiveMagazines;
        [SerializeField] private int _minMagazinesCount;
        [SerializeField] private int _maxMagazinesCount;
        
        private PlayerExperience _playerExperience;
        private PlayerShooting _playerShooting;
        private Weapon.Weapon _randomWeapon;

        public void Init(PlayerExperience playerExperience, PlayerShooting playerShooting)
        {
            _playerExperience = playerExperience;
            _playerShooting = playerShooting;
        }

        public void IncreaseExperience(int value)
        {
            if(value > 0)
               _experience += value;
        }
        
        public void RewardExperienceToPlayer()
        {
            _playerExperience.AddExperience(_experience);
        }

        public void RewardMagazinesToPlayer()
        {
            _randomWeapon = _playerShooting.GetRandomWeapon();

            int minRandomNumber = 0;
            int maxRandomNumber = 100;
            int randomNumber = Random.Range(minRandomNumber, maxRandomNumber);

            if (randomNumber <= _chanceToGiveMagazines)
            {
                var magazinesDropbox = Instantiate(_magazinesDropBox, transform.position, Quaternion.identity);
                magazinesDropbox.transform.Rotate(0, 0, 90);
                magazinesDropbox.transform.SetParent(null);
                
                int magazinesCount = Random.Range(_minMagazinesCount, _maxMagazinesCount);
                magazinesDropbox.Init(magazinesCount, _randomWeapon);
            }
        }
    }
}