using System.Collections.Generic;
using System.Linq;
using Player;
using UI;
using UnityEngine;
using UnityEngine.UI;
using UpgradedCards;

public class UpgradeCardsCreator : MonoBehaviour
{
    [SerializeField] private Image _cardsPanel;
    [SerializeField] private List<UpgradedCard> _upgradeCards = new();
    [SerializeField] private PlayerCardsCollector _playerCardsCollector;
    [SerializeField] private PlayerExperience _playerExperience;
    [SerializeField] private FirstPersonCamera _camera;
    [SerializeField] private int _upgradeCardsToChoose;

    private readonly List<UpgradedCard> _generatedCards = new();

    private void Awake()
    {
        _cardsPanel.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _playerExperience.LevelChanged += OnLevelChanged;
    }

    private void OnDisable()
    {
        _playerExperience.LevelChanged -= OnLevelChanged;
    }

    private void OnLevelChanged()
    {
        if (_playerExperience.Level > 1)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            
            Time.timeScale = 0;
            _camera.enabled = false;
            
            _cardsPanel.gameObject.SetActive(true);

            List<UpgradedCard> deletedCards = new();

            if (_upgradeCards.Count < _upgradeCardsToChoose)
                return;

            if (_playerCardsCollector.IsMaxTypes())
                _upgradeCards = _playerCardsCollector.GetCardTypes();
            
            for (int i = 0; i < _upgradeCardsToChoose; i++)
            {
                int randomCardNumber = Random.Range(0, _upgradeCards.Count);
                
                var randomCard = _upgradeCards[randomCardNumber];
                randomCard.gameObject.SetActive(true);
                randomCard.transform.localScale = Vector3.one;
                randomCard.transform.localRotation = Quaternion.identity;

                _upgradeCards.Remove(randomCard);
                deletedCards.Add(randomCard);
                
                var newCard = Instantiate(randomCard, _cardsPanel.transform);
                _generatedCards.Add(newCard);

                if (newCard.TryGetComponent(out UpgradedCardView upgradeCardView))
                    upgradeCardView.Click += OnClick;
                
                newCard.SetCardLevel(_playerCardsCollector.GetLevel(randomCard));
            }
            
            _upgradeCards.AddRange(deletedCards);
        }
    }

    private void OnClick()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        Time.timeScale = 1;
        _camera.enabled = true;
        
        _cardsPanel.gameObject.SetActive(false);

        var chosenCard = _generatedCards.FirstOrDefault(card => card.IsChosen);
        
        if (chosenCard != null)
        {
            _playerCardsCollector.TakeCard(chosenCard);
            chosenCard.SetCardLevel(_playerCardsCollector.GetLevel(chosenCard));
            chosenCard.SetLevelBorder(_playerCardsCollector.GetLevelBorder(chosenCard));
            
            if (chosenCard.TryGetComponent(out ExperienceCard experienceCard))
            {
                experienceCard.GiveExperienceToPlayer(_playerExperience);
            }
            else if (chosenCard.TryGetComponent(out HealthCard healthCard))
            {
                if(_playerExperience.TryGetComponent(out PlayerHealth playerHealth))
                   healthCard.GiveHealthToPlayer(playerHealth);
            }
            else if (chosenCard.TryGetComponent(out DamageCard damageCard))
            {
                damageCard.GiveDamageToPlayer();
            }
            else if (chosenCard.TryGetComponent(out PistolBulletsCard pistolBulletsCard))
            {
                if (_playerExperience.TryGetComponent(out PlayerShooting playerShooting))
                    pistolBulletsCard.GiveBulletsToPlayer(playerShooting.CurrentWeapon, playerShooting.GetPistol());
            }
            else if (chosenCard.TryGetComponent(out RiffleBulletsCard riffleBulletsCard))
            {
                if (_playerExperience.TryGetComponent(out PlayerShooting playerShooting))
                    riffleBulletsCard.GiveBulletsToPlayer(playerShooting.CurrentWeapon, playerShooting.GetRiffle());
            }
            else if (chosenCard.TryGetComponent(out PistolMagazinesCard pistolMagazinesCard))
            {
                if (_playerExperience.TryGetComponent(out PlayerShooting playerShooting))
                    pistolMagazinesCard.GiveMagazinesToPlayer(playerShooting.CurrentWeapon, playerShooting.GetPistol());
            }
            else if (chosenCard.TryGetComponent(out RiffleMagazinesCard riffleMagazinesCard))
            {
                if (_playerExperience.TryGetComponent(out PlayerShooting playerShooting))
                    riffleMagazinesCard.GiveMagazinesToPlayer(playerShooting.CurrentWeapon, playerShooting.GetRiffle());
            }
            else if(chosenCard.TryGetComponent(out MovementCard movementCard))
            {
                if(_playerExperience.TryGetComponent(out PlayerMovement playerMovement))
                    movementCard.GiveSpeedToPlayer(playerMovement);
            }
            else if (chosenCard.TryGetComponent(out JumpCard jumpCard))
            {
                if(_playerExperience.TryGetComponent(out PlayerMovement playerMovement))
                    jumpCard.GiveJumpSpeedToPlayer(playerMovement);
            }
        }

        foreach (var generatedCard in _generatedCards)
        {
            if(generatedCard.TryGetComponent(out UpgradedCardView upgradeCardView))
                upgradeCardView.Click -= OnClick;

            if (generatedCard.IsChosen)
            {
                generatedCard.transform.SetParent(_playerExperience.transform);
                generatedCard.gameObject.SetActive(false);
            }
            else
            {
                Destroy(generatedCard.gameObject);
            }
        }

        _generatedCards.Clear();
    }
}