using System.Collections.Generic;
using System.Linq;
using Enemy;
using UnityEngine;

public class EnemiesPool : MonoBehaviour
{
    private readonly List<EnemyAttacker> _pool = new();

    protected void Init(EnemyAttacker enemy, int count, Vector3 spawnPoint)
    {
        for (int i = 0; i < count; i++)
        {
            var newEnemy = Instantiate(enemy, spawnPoint, Quaternion.identity);
            newEnemy.gameObject.SetActive(false);
            newEnemy.transform.SetParent(transform);
            
            _pool.Add(newEnemy);
        }
    }

    protected bool TryGetEnemy(out EnemyAttacker enemy)
    {
        enemy = _pool.FirstOrDefault(result => result.gameObject.activeSelf == false);

        return enemy != null;
    }

    protected List<EnemyAttacker> GetEnemiesInPool()
    {
        return _pool;
    }
}