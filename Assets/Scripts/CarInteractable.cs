using System;
using UnityEngine;

public class CarInteractable : Interactable
{
    public Camera playerCamera;
    public Camera carCamera;
    public Transform LeavePoint;
    private bool inCar = false;

    private void Awake(){
        playerCamera.enabled = true;
        carCamera.enabled = false;
        FindFirstObjectByType<SCC_InputProcessor>().enabled = false;
        FindFirstObjectByType<FPSController>().enabled = true;
        playerCamera.GetComponent<AudioListener>().enabled = true;
        carCamera.GetComponent<AudioListener>().enabled = false;
        inCar = false;
    }

    public override void Interact(GameObject interactor)
    {
        if (inCar)
        {
            LeaveCar();
        }else
        {
            EnterCar();
        }
    }

    public void EnterCar()
    {
        playerCamera.enabled = false;
        carCamera.enabled = true;
        FindFirstObjectByType<SCC_InputProcessor>().enabled = true;
        FindFirstObjectByType<FPSController>().enabled = false;
        playerCamera.GetComponent<AudioListener>().enabled = false;
        carCamera.GetComponent<AudioListener>().enabled = true;
        inCar = true;
    }

    public void LeaveCar()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = LeavePoint.transform.localPosition;
        playerCamera.enabled = true;
        carCamera.enabled = false;
        FindFirstObjectByType<SCC_InputProcessor>().enabled = false;
        FindFirstObjectByType<FPSController>().enabled = true;
        playerCamera.GetComponent<AudioListener>().enabled = true;
        carCamera.GetComponent<AudioListener>().enabled = false;
        inCar = false;
        Debug.Log($"Player Position: {GameObject.FindGameObjectWithTag("Player").transform.position}");
        Debug.Log($"Leave point Position: {LeavePoint.transform.localPosition}");
    }
}
