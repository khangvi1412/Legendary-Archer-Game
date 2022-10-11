using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class SwitchCamera : MonoBehaviour
{
    
    public GameObject aimVirtualCamera;
    public GameObject thirdPersonCamera;
 
    public PlayerInput playerInput;

    public Canvas normalAimCanvas;

    public Canvas aimCanvas;

    InputAction aimAction;
    // Start is called before the first frame update
    void Awake()
    {
        //aimVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        aimAction = playerInput.actions["aim"];
    }
    private void OnEnable() {
        aimAction.performed += _ => StartAim();
        aimAction.canceled += _ => CancelAim();
    }

    private void OnDisable() {
        aimAction.performed -= _ => StartAim();
        aimAction.canceled -= _ => CancelAim();
    }

    void StartAim() {
        normalAimCanvas.enabled = false;
        aimCanvas.enabled = true;
        // aimVirtualCamera.Priority += 10;
        aimVirtualCamera.SetActive(true);
        thirdPersonCamera.SetActive(false);

    }
    void CancelAim() {
        normalAimCanvas.enabled = true;
        aimCanvas.enabled = false;
        // aimVirtualCamera.Priority -= 10;
        aimVirtualCamera.SetActive(false);
        thirdPersonCamera.SetActive(true);
    }
}
