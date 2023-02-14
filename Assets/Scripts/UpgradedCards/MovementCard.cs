using Player;
using UnityEngine;

namespace UpgradedCards
{
    public class MovementCard : UpgradedCard
    {
        [SerializeField] private float _increasedSpeed;
        
        public void GiveSpeedToPlayer(PlayerMovement playerMovement)
        {
            playerMovement.IncreaseMovementSpeed(_increasedSpeed);
        }
    }
}