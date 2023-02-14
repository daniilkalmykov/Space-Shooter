using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenuButton : CanvasButton
    {
        protected override void OnClick()
        {
            SceneManager.LoadScene(0);
        }
    }
}