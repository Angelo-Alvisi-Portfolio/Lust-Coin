using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void ExitGame() {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
