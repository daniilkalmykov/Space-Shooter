using Player;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class DiePanel : MonoBehaviour
    {
        [SerializeField] private AudioListener _audioListener;
        [SerializeField] private FirstPersonCamera _firstPersonCamera;
        [SerializeField] private PlayerHealth _playerHealth;

        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0;
        }

        private void OnEnable()
        {
            _playerHealth.Died += OnDied;
        }

        private void OnDisable()
        {
            _playerHealth.Died -= OnDied;
        }

        private void OnDied()
        {
            _canvasGroup.alpha = 1;
            
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;

            _firstPersonCamera.enabled = false;
            _audioListener.enabled = false;
        }
    }
}
