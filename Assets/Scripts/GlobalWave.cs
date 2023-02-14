using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GlobalWave : MonoBehaviour
{
    [SerializeField] private List<Spawner> _spawners = new();
    [SerializeField] private int _workingSpawnersCount;
    [SerializeField] private float _delay;
    [SerializeField] private float _increaseDelayValue;
    [SerializeField] private int _increaseEnemyHealthValue;
    [SerializeField] private int _increaseEnemyDamageValue;
    [SerializeField] private int _increaseEnemyExperienceValue;
    
    private readonly List<Spawner> _workingSpawners = new();
    
    private Coroutine _coroutine;

    private void OnEnable()
    {
        foreach (var spawner in _spawners)
            spawner.TurnedOff += OnTurnedOff;
    }

    private void OnDisable()
    {
        foreach (var spawner in _spawners)
            spawner.TurnedOff -= OnTurnedOff;
    }

    private void Start()
    {
        SetWorkingSpawners();
    }

    private void SetWorkingSpawners()
    {
        while (_workingSpawnersCount > _spawners.Count)
            _workingSpawnersCount--;
            
        foreach(var spawner in _spawners)
            spawner.gameObject.SetActive(false);
        
        for (int i = 0; i < _workingSpawnersCount; i++)
        {
            int randomSpawnerNumber = Random.Range(0, _spawners.Count);

            while (_workingSpawners.Any(spawner => spawner == _spawners[randomSpawnerNumber])) 
                randomSpawnerNumber = Random.Range(0, _spawners.Count);
            
            _workingSpawners.Add(_spawners[randomSpawnerNumber]); 
        }

        foreach (var workingSpawner in _workingSpawners)
            workingSpawner.gameObject.SetActive(true);            
    }

    private void SetNewWave()
    {
        foreach (var spawner in _workingSpawners)
            spawner.ResetOptions();
        
        _workingSpawners.Clear();
        
        SetWorkingSpawners();
    }

    private void OnTurnedOff()
    {
        if (_workingSpawners.All(spawner => spawner.CurrentWave == null))
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            _coroutine = StartCoroutine(WaitTime());
        }
    }
    
    private IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(_delay);

        foreach (var spawner in _spawners)
            spawner.IncreaseEnemiesStats(_increaseEnemyExperienceValue, _increaseEnemyDamageValue, _increaseEnemyHealthValue);

        _delay += _increaseDelayValue;
        
        SetNewWave();
    }
}