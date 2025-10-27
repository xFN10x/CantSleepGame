using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public Camera Camera;

    private PlayerInput input;
    private Rigidbody rigid;
    private InputAction cameraActions;
    private InputAction walkActions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        input = GetComponent<PlayerInput>();
        cameraActions = input.actions.FindAction("Look");
        walkActions = input.actions.FindAction("Move");
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cameraRead = cameraActions.ReadValue<Vector2>() / 10;
        Vector2 moveRead = walkActions.ReadValue<Vector2>() * 5;

        //camera movement
        transform.Rotate(new Vector3(0, cameraRead.x, 0)); //rotate the front of the player
        Camera.transform.Rotate(new Vector3(cameraRead.y * -1, 0, 0));
        float x = Camera.transform.localEulerAngles.x;
        if (x > 180)
            x -= 360;
        x = Mathf.Clamp(x, -50, 46);
        Camera.transform.localEulerAngles = new Vector3(x, 0, 0);

        //move player
        rigid.linearVelocity = 400 * Time.deltaTime * (transform.right * moveRead.x + transform.forward * moveRead.y).normalized + new Vector3(0, rigid.linearVelocity.y, 0);
    }
}
