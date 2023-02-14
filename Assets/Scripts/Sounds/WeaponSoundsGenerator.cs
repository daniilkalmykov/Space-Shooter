using UnityEngine;

namespace Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class WeaponSoundsGenerator : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        
        public void PlayAudioClip(AudioClip audioClip)
        {
            if(_audioSource.isPlaying)
                _audioSource.Stop();
            
            _audioSource.PlayOneShot(audioClip);
        }
    }
}