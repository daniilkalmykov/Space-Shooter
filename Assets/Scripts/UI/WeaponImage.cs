using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WeaponImage : MonoBehaviour
    {
        [SerializeField] private PlayerShooting _playerShooting;
        
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        private void OnEnable()
        {
            _playerShooting.WeaponChanged += OnWeaponChanged;
        }

        private void OnDisable()
        {
            _playerShooting.WeaponChanged -= OnWeaponChanged;
        }

        private void OnWeaponChanged(Sprite icon)
        {
            _image.sprite = icon;
            _image.SetNativeSize();
        }
    }
}
