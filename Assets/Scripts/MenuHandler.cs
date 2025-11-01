using UnityEditor;
using UnityEngine;

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

    public void Start()
    {
        MainCanvas.enabled = false;
    }
}
