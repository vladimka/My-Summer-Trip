using System;
using UnityEngine;

public class CarInteractable : Interactable
{
    public Camera playerCamera;
    public Camera carCamera;
    public Transform LeavePoint;
    private GameObject player;
    private bool inCar = false;

    private void Awake(){
        playerCamera.enabled = true;
        carCamera.enabled = false;
        FindFirstObjectByType<SCC_InputProcessor>().enabled = false;
        FindFirstObjectByType<FPSController>().enabled = true;
        playerCamera.GetComponent<AudioListener>().enabled = true;
        carCamera.GetComponent<AudioListener>().enabled = false;
        inCar = false;
        player = GameObject.FindGameObjectWithTag("Player");
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
        foreach(var r in player.GetComponentsInChildren<Renderer>())
            r.enabled = false;
        player.GetComponent<CharacterController>().enabled = false;
    }

    public void LeaveCar()
    {
        foreach(var r in player.GetComponentsInChildren<Renderer>())
            r.enabled = true;
        player.GetComponent<CharacterController>().enabled = true;
        FindFirstObjectByType<CharacterController>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").transform.position = LeavePoint.transform.position;
        FindFirstObjectByType<CharacterController>().enabled = true;
        playerCamera.enabled = true;
        carCamera.enabled = false;
        FindFirstObjectByType<SCC_InputProcessor>().enabled = false;
        FindFirstObjectByType<FPSController>().enabled = true;
        playerCamera.GetComponent<AudioListener>().enabled = true;
        carCamera.GetComponent<AudioListener>().enabled = false;
        inCar = false;
    }
}
