using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class PickupInteractable : Interactable
{
    public float holdDistance = 1.5f;

    private Rigidbody rb;
    private Collider col;

    private Transform holdPoint;
    private bool isHeld;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    public override void Interact(GameObject interactor)
    {
        if (isHeld)
            Drop();
        else
            PickUp(interactor);
    }

    private void PickUp(GameObject interactor)
    {
        Camera cam = interactor.GetComponentInChildren<Camera>();
        if (cam == null) return;

        holdPoint = cam.transform.Find("HoldPoint");
        if (holdPoint == null)
        {
            Debug.LogError("HoldPoint not found on Camera");
            return;
        }

        isHeld = true;

        rb.isKinematic = true;
        col.enabled = false;

        transform.SetParent(holdPoint);
        transform.localPosition = Vector3.forward * holdDistance;
        transform.localRotation = Quaternion.identity;

        prompt = "Drop";
    }

    private void Drop()
    {
        isHeld = false;

        transform.SetParent(null);

        rb.isKinematic = false;
        col.enabled = true;

        prompt = "Pick up";
    }
}
