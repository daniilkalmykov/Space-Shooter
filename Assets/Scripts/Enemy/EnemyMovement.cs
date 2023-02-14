using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    [RequireComponent(typeof(NavMeshAgent), typeof(EnemyAttacker))]
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float _minDistanceBetweenPlayerAndEnemy;
        
        private PlayerHealth _target;
        private NavMeshAgent _navMeshAgent;
        private EnemyAttacker _enemyAttacker;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _enemyAttacker = GetComponent<EnemyAttacker>();
        }

        private void Start()
        {
            _target = _enemyAttacker.PlayerHealth;
        }

        private void Update()
        {
            if(_target == null)
                _navMeshAgent.destination = transform.position;
            
            _navMeshAgent.destination = _target.transform.position;

            if (Vector3.Distance(transform.position, _target.transform.position) <= _minDistanceBetweenPlayerAndEnemy)
                _enemyAttacker.Attack();
        }
    }
}