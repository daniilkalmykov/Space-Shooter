using Sounds;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button), typeof(ButtonClickSoundTurntable))]
    public abstract class CanvasButton : MonoBehaviour
    {
        private Button _button;
        private ButtonClickSoundTurntable _buttonClickSoundTurntable;
        
        private void Awake()
        {
            _button = GetComponent<Button>();
            _buttonClickSoundTurntable = GetComponent<ButtonClickSoundTurntable>();
        }
        
        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);    
            _button.onClick.AddListener(_buttonClickSoundTurntable.OnClick);    
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
            _button.onClick.RemoveListener(_buttonClickSoundTurntable.OnClick);
        }

        protected abstract void OnClick();
    }
}