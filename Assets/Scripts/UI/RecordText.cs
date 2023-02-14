using Player;
using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class RecordText : MonoBehaviour
    {
        [SerializeField] private PlayerExperience _playerExperience;
        
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            _playerExperience.RecordChanged += OnRecordChanged;
        }

        private void OnDisable()
        {
             _playerExperience.RecordChanged += OnRecordChanged;
        }

        private void OnRecordChanged(int record)
        {
            _text.text = record.ToString();
        }
    }
}