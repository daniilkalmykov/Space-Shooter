using UnityEngine;
using Weapon;

namespace UpgradedCards
{
    public class DamageCard : UpgradedCard
    {
        [SerializeField] private Bullet _bullet;
        [SerializeField] private int _increaseDamageValue;

        public void GiveDamageToPlayer()
        {
            _bullet.IncreaseDamage(_increaseDamageValue);
        }
    }
}