using UnityEngine;

namespace UpgradedCards
{
    public abstract class MagazinesCard : UpgradedCard
    {
        [SerializeField] private int _increaseMagazinesCount;

        public void GiveMagazinesToPlayer(Weapon.Weapon currentWeapon, Weapon.Weapon weapon)
        {
            weapon.AddMagazinesCount(_increaseMagazinesCount);

            if (currentWeapon == weapon)
                weapon.StartMagazinesCountChangedAction();                
        }
    }
}
