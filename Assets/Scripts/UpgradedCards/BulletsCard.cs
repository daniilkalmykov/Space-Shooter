using UnityEngine;

namespace UpgradedCards
{
    public abstract class BulletsCard : UpgradedCard
    {
        [SerializeField] private int _increaseMaxBulletsValue;

        public void GiveBulletsToPlayer(Weapon.Weapon currentWeapon, Weapon.Weapon weapon)
        {
            weapon.IncreaseMaxBulletsCount(_increaseMaxBulletsValue);

            if (currentWeapon == weapon) 
                weapon.StartBulletsCountChangedAction();                
        }
    }
}