using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(EnemyReward))]
    public class EnemyHealth : Health
    {
        private EnemyReward _enemyReward;

        private void Awake()
        {
            _enemyReward = GetComponent<EnemyReward>();
        }

        protected override void Die()
        {
            gameObject.SetActive(false);
            
            _enemyReward.RewardExperienceToPlayer();
            _enemyReward.RewardMagazinesToPlayer();
        }
    }
}