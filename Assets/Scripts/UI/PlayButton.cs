using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class PlayButton : CanvasButton
    {
        protected override void OnClick()
        {
            SceneManager.LoadScene(1);
            Time.timeScale = 1;
        }
    }
}