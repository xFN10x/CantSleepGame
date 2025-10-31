using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public Camera Camera;

    public TextMeshProUGUI InteractText;
    public RawImage CrossHair;
    public Texture NormalImage;
    public Texture InteractImage;
    public bool ControlsEnabled;

    //public GameObject ViewModelChlid;

    public GameObject Gun;

    private PlayerInput input;
    private Rigidbody rigid;
    private InputAction cameraActions;
    private InputAction walkActions;

    private InteractableObject currentInteractableAction = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Gun.transform.localPosition = new Vector3(0.633000016f, -0.407999992f, -1.02900004f);
        Gun.transform.localEulerAngles = new Vector3(0, 270, 270);
        Cursor.lockState = CursorLockMode.Locked;
        input = GetComponent<PlayerInput>();
        cameraActions = input.actions.FindAction("Look");
        walkActions = input.actions.FindAction("Move");
        input.actions.FindAction("Interact").performed += Interacted;
        rigid = GetComponent<Rigidbody>();
    }

    private void Interacted(InputAction.CallbackContext obj)
    {
        if (currentInteractableAction == null) return;
        currentInteractableAction.DoAction();
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main != Camera && Gun.activeInHierarchy)
        {
            Gun.SetActive(false);
        }
        else if (!Gun.activeInHierarchy && Camera.main == Camera)
        {
            Gun.SetActive(true);
        }
        //ViewModelChlid.transform.localEulerAngles = Camera.transform.localEulerAngles;
        if (ControlsEnabled)
        {
            Vector2 cameraRead = cameraActions.ReadValue<Vector2>() / 10;
            Vector2 moveRead = walkActions.ReadValue<Vector2>() * 5;

            //camera movement
            transform.Rotate(new Vector3(0, cameraRead.x, 0)); //rotate the front of the player
            Camera.transform.Rotate(new Vector3(cameraRead.y * -1, 0, 0));
            float x = Camera.transform.localEulerAngles.x;
            if (x > 180)
                x -= 360;
            x = Mathf.Clamp(x, -60, 70);
            Camera.transform.localEulerAngles = new Vector3(x, 0, 0);

            //move player
            rigid.linearVelocity = 4 * (transform.right * moveRead.x + transform.forward * moveRead.y).normalized + new Vector3(0, rigid.linearVelocity.y, 0);

            //Debug.DrawLine(Camera.transform.position, Camera.transform.TransformDirection(Vector3.forward) * 10f);
            if (Physics.Raycast(Camera.transform.position, Camera.transform.TransformDirection(Vector3.forward), out RaycastHit hit, 3f))
            {
                //print(hit.transform.gameObject.name);
                if (hit.transform.gameObject.TryGetComponent<InteractableObject>(out InteractableObject otherController))
                {
                    CrossHair.texture = InteractImage;
                    InteractText.text = otherController.InteractText;
                    currentInteractableAction = otherController;
                }
                else
                {
                    currentInteractableAction = null;
                    CrossHair.texture = NormalImage;
                    InteractText.text = "";
                }
            }
            else
            {
                currentInteractableAction = null;
                CrossHair.texture = NormalImage;
                InteractText.text = "";
            }
        }
        else
        {
            currentInteractableAction = null;
            CrossHair.texture = NormalImage;
            InteractText.text = "";
        }
    }
}
