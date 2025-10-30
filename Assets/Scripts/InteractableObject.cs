using UnityEngine;
using static GameController;

[RequireComponent(typeof(Rigidbody))]
public class InteractableObject : MonoBehaviour
{
    public string InteractText;
    public InteractAction Interaction;

    public void DoAction()
    {
        GameController.CurrentGameController.DoInteract(Interaction);
    }
}
