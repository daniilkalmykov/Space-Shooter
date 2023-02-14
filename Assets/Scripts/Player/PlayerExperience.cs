using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerExperience : MonoBehaviour
    {
        private const int TimeToGetNewLevel = 1;
        
        [SerializeField] private int _addingIncreaseExperience;
        [SerializeField] private Record _record;

        public event Action<float, float> ExperienceChanged;
        public event Action<int> RecordChanged;
        public event Action LevelChanged;

        private Coroutine _coroutine;        
        private int _increaseExperienceToNextLevel = 100;
        
        public int Experience { get; private set; }
        public int ExperienceToNextLevel { get; private set; } = 100;
        public int Level { get; private set; } = 1;

        private void Start()
        {
            LevelChanged?.Invoke();
            RecordChanged?.Invoke(_record.HighestRecord);
        }

        /*private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
                AddExperience(ExperienceToNextLevel);
        }*/

        public void AddExperience(int reward)
        {
            Experience += reward;

            if (Experience >= ExperienceToNextLevel)
            {
                Level++;

                Experience = 0;
                
                _increaseExperienceToNextLevel += _addingIncreaseExperience;
                ExperienceToNextLevel += _increaseExperienceToNextLevel;
                
                LevelChanged?.Invoke();

                if (_record.HighestRecord < Level)
                {
                    _record.SetNewRecord(Level);
                    RecordChanged?.Invoke(_record.HighestRecord);
                }
            }
            
            ExperienceChanged?.Invoke(Experience, ExperienceToNextLevel);
        }

        public void StartWaitingCoroutine()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            
            _coroutine = StartCoroutine(WaitTimeToReachNextLevel());
        }
        
        private  IEnumerator WaitTimeToReachNextLevel()
        {
            yield return new WaitForSeconds(TimeToGetNewLevel);

            AddExperience(ExperienceToNextLevel);
        }
    }
}