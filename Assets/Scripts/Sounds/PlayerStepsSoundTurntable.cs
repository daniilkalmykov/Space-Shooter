using UnityEngine;

namespace Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerStepsSoundTurntable : MonoBehaviour
    {
        private const float TimeToStep = 0.5f;
        
        private AudioSource _audioSource;
        private float _stepTimer;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayStepAudioClip()
        {
            _stepTimer += Time.deltaTime;

            if (_stepTimer >= TimeToStep)
            {
                _audioSource.Play();
                _stepTimer = 0;
            }
        }

        public void StopStepsSound()
        {
            if (_audioSource.isPlaying)
                _audioSource.Stop();
        }
    }
}