using Player;
using UnityEngine;

namespace UpgradedCards
{
    public class HealthCard : UpgradedCard
    {
        [SerializeField] private int _increaseHealthValue;
        [SerializeField] private int _blockingDamageTimer;

        public void GiveHealthToPlayer(PlayerHealth playerHealth)
        {
            if (Level > InitialLevel)  
                _increaseHealthValue += _increaseHealthValue * Level;

            if (Level == LevelBorder)
            {
                ReachBorder();
                
                playerHealth.BlockDamage(_blockingDamageTimer);
                _blockingDamageTimer += _blockingDamageTimer;
            }
            else
            {
                playerHealth.IncreaseMaxHealth(_increaseHealthValue);
            }
        }
    }
}