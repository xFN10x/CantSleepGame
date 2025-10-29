using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Camera CutsceneCamera;
    public Image FadePanel;
    public TextMeshProUGUI DialougeText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(StartingCutscene());
    }

    IEnumerator StartingCutscene()
    {
        yield return new WaitForSeconds(3);
        yield return FadePanel.DOFade(1f, 3f).WaitForCompletion();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
