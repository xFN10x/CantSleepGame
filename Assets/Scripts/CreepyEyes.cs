using DG.Tweening;
using System.Collections;
using UnityEngine;
using static GameController;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider))]
public class CreepyEyes : MonoBehaviour
{
    public ParticleSystem Eye1;
    public ParticleSystem Eye2;
    public Transform Billboared;

    public bool ProximityTriggered = true;
    public bool Move = true;

    public bool DoInteractAction = false;
    public bool ForceLook = true;
    public InteractAction Action;

    private AudioSource source;
    private Collider coll;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && ProximityTriggered)
        {
            DoCreepy();
        }
    }

    private void FixedUpdate()
    {
        if (Billboared != null)
            Billboared.LookAt(GameController.CurrentGameController.Player.transform);
    }
    private void Start()
    {
        coll = GetComponent<Collider>();
        coll.isTrigger = true;
        source = GetComponent<AudioSource>();
    }

    IEnumerator MakePlrLook()
    {
        PlayerController plr = GameController.CurrentGameController.Player;
        plr.ControlsEnabled = false;
        plr.transform.LookAt(transform);
        plr.Camera.transform.LookAt(transform);
        plr.Camera.DOFieldOfView(30, 1f);
        plr.Camera.transform.localEulerAngles = new Vector3(plr.Camera.transform.localEulerAngles.x, 0, 0);
        plr.transform.localEulerAngles = new Vector3(0, plr.transform.localEulerAngles.y, 0);
        for (int i = 0; i < 200; i++)
        {
            yield return new WaitForEndOfFrame();
            plr.Camera.transform.LookAt(transform);
        }
        plr.ControlsEnabled = true;
        plr.Camera.DOFieldOfView(70, 1f);
    }
    public void DoCreepy()
    {
        if (ForceLook)
        {
            StartCoroutine(MakePlrLook());
        }
        Eye1.Play();
        Eye2.Play();
        source.Play();
        ProximityTriggered = false;
        if (Move)
            transform.DOMoveX(transform.position.x + 10, 1f);

        if (DoInteractAction)
            GameController.CurrentGameController.DoInteract(Action);
    }
}
