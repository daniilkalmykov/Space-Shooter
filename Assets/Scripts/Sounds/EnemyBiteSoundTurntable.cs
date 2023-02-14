using UnityEngine;

namespace Sounds
{
    [RequireComponent(typeof(EnemyBiteSoundTurntable))]
    public class EnemyBiteSoundTurntable : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayBiteAudioClip()
        {
            if (_audioSource.isPlaying)
                _audioSource.Stop();
            
            _audioSource.Play();
        }

        public void StopSounds()
        {
            _audioSource.mute = true;
        }
    }
}