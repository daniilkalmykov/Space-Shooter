using System.Collections;
using UnityEngine;

namespace Sounds
{
    [RequireComponent(typeof(AudioSource))]
    public class RandomSoundsCreator : MonoBehaviour
    {
        [SerializeField] private AudioClip[] _randomAudioClips;
        [SerializeField] private float _delay;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            StartCoroutine(WaitTimeBetweenAudioClips(_delay));
        }

        private void PlayRandomAudioClip()
        {
            int randomAudioClipNumber = Random.Range(0, _randomAudioClips.Length);
            
            _audioSource.PlayOneShot(_randomAudioClips[randomAudioClipNumber]);
            StartCoroutine(WaitTimeBetweenAudioClips(_delay + _randomAudioClips[randomAudioClipNumber].length));
        }

        private IEnumerator WaitTimeBetweenAudioClips(float delay)
        {
            yield return new WaitForSeconds(delay);
            
            PlayRandomAudioClip();
        }
    }
}
