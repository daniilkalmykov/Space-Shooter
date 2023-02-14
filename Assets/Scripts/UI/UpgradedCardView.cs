using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UpgradedCards;

namespace UI
{
    [RequireComponent(typeof(UpgradedCard), typeof(Button))]
    public class UpgradedCardView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _level;

        public event Action Click;
        
        private UpgradedCard _upgradedCard;
        private Button _button;

        private void Awake()
        {
            _upgradedCard = GetComponent<UpgradedCard>();
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);

            _upgradedCard.LevelChanged += OnLevelChanged;
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);

            _upgradedCard.LevelChanged -= OnLevelChanged;
        }

        private void OnClick()
        {
            _upgradedCard.Choose();
            
            Click?.Invoke();
        }

        private void OnLevelChanged(int value)
        {
            ShowCardLevel(value);
        }

        private void ShowCardLevel(int value)
        {
            _level.text = value.ToString();
        }
    }
}