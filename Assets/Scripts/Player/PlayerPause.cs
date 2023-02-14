using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerPause : MonoBehaviour
    {
        [SerializeField] private AudioListener _audioListener;
        [SerializeField] private Image _pausePanel;
        [SerializeField] private FirstPersonCamera _firstPersonCamera;

        public bool IsPaused { get; private set; }

        private void Awake()
        {
            _pausePanel.gameObject.SetActive(false);
        }

        public void Pause()
        {
            ChangePauseState(true);
        }

        public void Unpause()
        {
            ChangePauseState(false);
        }

        private void ChangePauseState(bool pauseState)
        {
            IsPaused = pauseState;

            //_audioListener.enabled = !pauseState;
            Time.timeScale = pauseState ? 0 : 1;
            _pausePanel.gameObject.SetActive(pauseState);
            _firstPersonCamera.enabled = !pauseState;
            
            Cursor.visible = _pausePanel;
            Cursor.lockState = pauseState ? CursorLockMode.Confined : CursorLockMode.Locked;
        }
    }
}
