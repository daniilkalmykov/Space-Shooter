using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Slider))]
    public abstract class Bar : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Slider _slider;
        private Coroutine _coroutine;    
    
        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }
        
        protected void SetStartValues(float maxValue, float currentValue)
        {
            _slider.maxValue = maxValue;
            _slider.value = currentValue;
        }
    
        protected void OnValueChanged(float currentValue, float maxValue)
        {
            _slider.maxValue = maxValue;
            
            if (_coroutine != null) 
                StopCoroutine(_coroutine);
            
            _coroutine = StartCoroutine(ChangeSliderValue(currentValue));
        }

        private IEnumerator ChangeSliderValue(float currentValue)
        {
            while (_slider.value != currentValue)
            {
                _slider.value = Mathf.MoveTowards(_slider.value, currentValue, Time.deltaTime * _speed);

                yield return null;
            }
        }
    }
}
