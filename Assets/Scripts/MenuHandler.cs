using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public Canvas MainCanvas;

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else   
            Application.Quit();
#endif
    }

    public void Play()
    {
        MainCanvas.enabled = false;
        SceneManager.LoadSceneAsync("Game", LoadSceneMode.Single);
    }
}
