using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UpgradedCards;

namespace Player
{
    public class PlayerCardsCollector : MonoBehaviour
    {
        [SerializeField] private int _maxCardTypesCount;
        
        private readonly Dictionary<UpgradedCard, int> _cards = new();
        private readonly Dictionary<UpgradedCard, int> _cardsLevelBorders = new();

        public void TakeCard(UpgradedCard chosenCard)
        {
            var foundPair = _cards.FirstOrDefault(pair => pair.Key.Id == chosenCard.Id);

            if (foundPair.Key == null)
            {
                _cards.Add(chosenCard, chosenCard.InitialLevel);
                _cardsLevelBorders.Add(chosenCard, chosenCard.LevelBorder);
            }
            else
            {
                _cards[foundPair.Key]++;

                chosenCard.LevelBorderChanged += OnLevelBorderChanged;

                if (chosenCard.transform.parent == transform)
                    chosenCard.LevelBorderChanged -= OnLevelBorderChanged;
            }
        }
        
        public int GetLevel(UpgradedCard chosenCard)
        {
            var foundCard = _cards.FirstOrDefault(pair => pair.Key.Id == chosenCard.Id);

            return foundCard.Key == null ? 0 : foundCard.Value;
        }

        public int GetLevelBorder(UpgradedCard chosenCard)
        {
            var foundCard = _cardsLevelBorders.FirstOrDefault(pair => pair.Key.Id == chosenCard.Id);

            return foundCard.Key == null ? 0 : foundCard.Value;
        }

        public bool IsMaxTypes()
        {
            return _maxCardTypesCount <= _cards.Count;
        }

        public List<UpgradedCard> GetCardTypes()
        {
            return new List<UpgradedCard>(_cards.Keys);
        }

        private void OnLevelBorderChanged(int id, int levelBorder)
        {
            var foundCard = _cardsLevelBorders.FirstOrDefault(pair => pair.Key.Id == id);

            _cardsLevelBorders[foundCard.Key] = levelBorder;
        }
    }
}