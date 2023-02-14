using Sounds;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ExitButton : CanvasButton
    {
        protected override void OnClick()
        {
            Application.Quit();
        }
    }
}