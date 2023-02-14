using Player;
using UnityEngine;

namespace UI
{
    public class ContinueButton : CanvasButton
    {
        [SerializeField] private PlayerPause _playerPause;

        protected override void OnClick()
        {
            _playerPause.Unpause();
        }
    }
}