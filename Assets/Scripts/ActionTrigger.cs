using UnityEngine;
using static GameController;

public class ActionTrigger : MonoBehaviour
{
    public InteractAction Action;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameController.CurrentGameController.DoInteract(Action);
        }
    }
}
