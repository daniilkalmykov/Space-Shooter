using Player;
using UnityEngine;

namespace UpgradedCards
{
    public class JumpCard : UpgradedCard
    {
        [SerializeField] private float _increasedSpeed;

        public void GiveJumpSpeedToPlayer(PlayerMovement playerMovement)
        {
            playerMovement.IncreaseJumpSpeed(_increasedSpeed);
        }
    }
}