using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionController : MonoBehaviour
{
    [Header("Raycast")]
    public Camera playerCamera;
    public LayerMask interactLayer;
    public float maxDistance = 3f;

    [Header("UI")]
    public InteractionUI interactionUI;

    private PlayerInputActions input;
    private Interactable currentInteractable;
    private PickupInteractable heldItem;
    private CarInteractable drivableCar;

    private void Awake()
    {
        input = new PlayerInputActions();
    }

    private void OnEnable()
    {
        input.Player.Enable();
        input.Player.Interact.performed += _ => TryInteract();
    }

    private void OnDisable()
    {
        input.Player.Disable();
    }

    private void Update()
    {
        if(FindFirstObjectByType<PauseMenuController>().IsPaused)
            return;

        if (heldItem != null)
            return;

        if(drivableCar != null)
            return;
        
        CheckForInteractable();
    }

    private void CheckForInteractable()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, interactLayer))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                currentInteractable = interactable;
                interactionUI.Show(interactable.prompt);
                return;
            }
        }

        currentInteractable = null;
        interactionUI.Hide();
    }

    private void TryInteract()
    {
        if(FindFirstObjectByType<PauseMenuController>().IsPaused)
            return;

        if (heldItem != null)
        {
            heldItem.Interact(gameObject);
            heldItem = null;
            return;
        }

        if(drivableCar != null)
        {
            drivableCar.Interact(gameObject);
            drivableCar = null;
            return;
        }

        if (currentInteractable is PickupInteractable pickup)
        {
            pickup.Interact(gameObject);
            heldItem = pickup;
        }
        else if(currentInteractable is CarInteractable car)
        {
            car.Interact(gameObject);
            drivableCar = car;
        }
        else if (currentInteractable != null)
        {
            currentInteractable.Interact(gameObject);
        }
    }
}