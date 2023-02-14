using Player;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ExperienceBar : Bar
    {
        [SerializeField] private TMP_Text _level;
        [SerializeField] private PlayerExperience _playerExperience;
        
        private void OnEnable()
        {
            _playerExperience.ExperienceChanged += OnValueChanged;
            _playerExperience.LevelChanged += OnLevelChanged;
        }

        private void OnDisable()
        {
            _playerExperience.ExperienceChanged -= OnValueChanged;
            _playerExperience.LevelChanged += OnLevelChanged;
        }
        
        private void Start()
        {
            SetStartValues(_playerExperience.ExperienceToNextLevel, _playerExperience.Experience);
        }

        private void OnLevelChanged()
        {
            SetStartValues(_playerExperience.ExperienceToNextLevel, 0);

            _level.text = _playerExperience.Level.ToString();
        }
    }
}