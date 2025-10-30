using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public enum InteractAction
    {
        BathroomAction,
        BedAction,
        DoorAction
    }
    public static GameController CurrentGameController;

    public PlayerController Player;

    public Camera CutsceneCamera;
    public Image FadePanel;
    public TextMeshProUGUI DialougeText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void DoInteract(InteractAction action)
    {
        switch (action)
        {
            /*case InteractAction.BathroomAction:
                break;
            case InteractAction.BedAction:
                break;
            case InteractAction.DoorAction:
                break;*/
            default:
                StartCoroutine(ShowText("Nothing happens."));
                break;
        }
    }

    private void Awake()
    {
        CurrentGameController = this;
    }
    void Start()
    {
        Player.Camera.enabled = false;
        StartCoroutine(StartingCutscene());
    }

    IEnumerator ShowText(string text)
    {
        DialougeText.text = "";
        DialougeText.color = Color.white;
        for (int i = 0; i < text.Length; i++)
        {
            yield return new WaitForEndOfFrame();
            DialougeText.text = text[..i];
        }
        yield return new WaitForSeconds(3);
        for (float i = 1f; i > 0f; i -= 0.01f)
        {
            yield return new WaitForEndOfFrame();
            DialougeText.alpha = i;
        }


    }
    IEnumerator StartingCutscene()
    {
        yield return new WaitForSeconds(2);
        yield return FadePanel.DOFade(0f, 3f).WaitForCompletion();
        yield return new WaitForSeconds(1);
        StartCoroutine(ShowText("You've woken up around 10 minutes ago, and you can't seem to get back to sleep."));
        yield return new WaitForSeconds(5);
        StartCoroutine(ShowText("Since you can't sleep currently, you should get out of bed."));
        yield return new WaitForSeconds(1);
        yield return CutsceneCamera.transform.DOMove(Player.Camera.transform.position, 2f).WaitForCompletion();
        Player.ControlsEnabled = true;
        Player.Camera.enabled = true;
        CutsceneCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
