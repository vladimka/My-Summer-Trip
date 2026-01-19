using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [Header("Interaction")]
    public string prompt = "Interact";

    public virtual void Interact(GameObject interactor)
    {
        Debug.Log($"{interactor.name} interacted with {prompt}");
    }
}
