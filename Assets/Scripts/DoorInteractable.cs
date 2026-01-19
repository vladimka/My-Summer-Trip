using UnityEngine;

public class DoorInteractable : Interactable
{
    [Header("Door Settings")]
    public Transform door;              // Меш двери
    public float openAngle = 90f;        // Угол открытия
    public float openSpeed = 5f;
    public bool openAwayFromPlayer = true;

    private bool isOpen;
    private Quaternion closedRotation;
    private Quaternion targetRotation;

    private void Awake()
    {
        if (door == null)
            door = transform;

        closedRotation = door.localRotation;
    }

    public override void Interact(GameObject interactor)
    {
        isOpen = !isOpen;

        if (isOpen)
            Open(interactor.transform);
        else
            Close();

        prompt = isOpen ? "Close" : "Open";
    }

    private void Open(Transform interactor)
    {
        float direction = 1f;

        if (openAwayFromPlayer)
        {
            Vector3 toPlayer = interactor.position - door.position;
            direction = Vector3.Dot(door.right, toPlayer) > 0 ? -1f : 1f;
        }

        targetRotation = closedRotation * Quaternion.Euler(0, openAngle * direction, 0);
        StopAllCoroutines();
        StartCoroutine(RotateDoor(targetRotation));
    }

    private void Close()
    {
        targetRotation = closedRotation;
        StopAllCoroutines();
        StartCoroutine(RotateDoor(targetRotation));
    }

    private System.Collections.IEnumerator RotateDoor(Quaternion target)
    {
        while (Quaternion.Angle(door.localRotation, target) > 0.1f)
        {
            door.localRotation = Quaternion.Slerp(
                door.localRotation,
                target,
                Time.deltaTime * openSpeed);

            yield return null;
        }

        door.localRotation = target;
    }
}
