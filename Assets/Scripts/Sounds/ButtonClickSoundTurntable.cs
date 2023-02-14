using UnityEngine;

namespace Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class ButtonClickSoundTurntable : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void OnClick()
        {
            _audioSource.Play();
        }
    }
}
