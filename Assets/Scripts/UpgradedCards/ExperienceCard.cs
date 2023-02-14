using Player;
using UnityEngine;

namespace UpgradedCards
{
    public class ExperienceCard : UpgradedCard
    {
        [SerializeField] private int _increaseExperienceReward;
        [SerializeField] private int _experienceComponent;

        private Coroutine _coroutine;

        public void GiveExperienceToPlayer(PlayerExperience playerExperience)
        {
            if (LevelBorder == Level)
            {
                playerExperience.StartWaitingCoroutine();                
                
                ReachBorder();
            }
            else
            {
                playerExperience.AddExperience(_increaseExperienceReward);
                _increaseExperienceReward += _experienceComponent;
            }
        }
    }
}