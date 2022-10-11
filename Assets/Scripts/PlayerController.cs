using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(CharacterController),typeof(PlayerInput))]


public class PlayerController : MonoBehaviour
{
    // how much you want to smooth between animations
    public float smoothTimeAnimation = 0.1f;
    public float speedRotation = 5f;
    public float playerSpeed = 2.0f;
    public float gravityValue = -9.81f;
    public float arrowHitMissDistance = 5f;
    public float attackRange = 35f;
    public float reloadTime = 1.65f;
    private CharacterController controller;
    private Vector3 playerVelocity;
    public GameObject arrowPrefab;
    public Transform aimTarget;
    public float targetDistance = 10.0f;
    PlayerInput playerInput;
    [Header("Input Actions")]
    InputAction moveAction;
    InputAction jumpAction;
    InputAction shootAction;
    InputAction runAction;
    InputAction aimAction;
    Transform cameraTransform;
    [Header("Animator and ID")]
    Animator animator;
    int forwardParameterId;
    int strafeParameterId;
    int jumpAnimation;
    int isRunParameterId;
    int isWalkingParameterId;
    int isAimingParameterId;
    int isPullStringParameterId;
    int fireParameterId;
    Vector2 currentAnimationBlendVector;
    Vector2 animationVelocity;
    public float animationTransition = 0.15f;
    bool runPressed;
    bool movementPressed;
    bool aimPressed;
    public BowController bowScript;
    Ray ray;
    RaycastHit hit;
    bool isReloading;
    public GameObject debugPositionRay;
    //go before enable and disable func
    private void Awake()
    {
        //lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        //gan bien action
        moveAction = playerInput.actions["move"];
        jumpAction = playerInput.actions["jump"];
        shootAction = playerInput.actions["shoot"];
        runAction = playerInput.actions["run"];
        aimAction = playerInput.actions["aim"];
        cameraTransform = Camera.main.transform;
        runAction.performed += ctx => runPressed = ctx.ReadValueAsButton();
        runAction.canceled += ctx => runPressed = ctx.ReadValueAsButton();
        aimAction.performed += ctx => aimPressed = ctx.ReadValueAsButton();
        aimAction.canceled += ctx => aimPressed = ctx.ReadValueAsButton();

        gameObject.tag = "Player";
    }
    void Start() {
        animator = GetComponent<Animator>();
        forwardParameterId = Animator.StringToHash("forward");
        strafeParameterId = Animator.StringToHash("strafe");
        jumpAnimation = Animator.StringToHash("dive forward");
        isRunParameterId = Animator.StringToHash("isRunning");
        isWalkingParameterId = Animator.StringToHash("isWalking");
        isAimingParameterId = Animator.StringToHash("isAim");
        isPullStringParameterId = Animator.StringToHash("isPullString");
        fireParameterId = Animator.StringToHash("fire");
        DisableArrow();
        isReloading = false;
    }

    private void OnEnable() {
        shootAction.performed += _ =>  {
            if(aimPressed) {
                Shoot();
            }
        };

    }
    private void OnDisable() {
       shootAction.performed -= _ => Shoot();
    }
    void MovementHanle() {
        bool isRunning = animator.GetBool(isRunParameterId);
        bool isWalking = animator.GetBool(isWalkingParameterId);
        if (isWalking && !isRunning && runPressed) {
            animator.SetBool(isRunParameterId,true);
            animator.SetBool(isWalkingParameterId,false);
        }
        if (!isWalking && isRunning && !runPressed) {
            animator.SetBool(isRunParameterId,false);
            animator.SetBool(isWalkingParameterId,true);
        }
    }

    void AimHandle() {
        bool isAim = animator.GetBool(isAimingParameterId);
        bool isPullString = animator.GetBool(isPullStringParameterId);
        
        if (aimPressed && !isAim) {
            animator.SetBool(isAimingParameterId,true);
        }
        if (!aimPressed) {
            animator.SetBool(isAimingParameterId,false);
            animator.SetBool(isPullStringParameterId,false);
            DisableArrow();
            Release();
        }
    }
    private void Shoot()
    {
        if(isReloading) return;
        if(!isReloading) {
            Vector3 camPosition = cameraTransform.position;
            Vector3 dir = cameraTransform.forward;
            animator.SetTrigger(fireParameterId);
            
            ray = new Ray(camPosition, dir);
            if(Physics.Raycast(ray, out hit, attackRange))
            {
                //debugPositionRay.transform.position = hit.point;
                Debug.Log("Raycast if");
                bowScript.Fire(hit.point, true);
            }
            else
            {
                Debug.Log("Raycast else");
                Vector3 target = cameraTransform.position + cameraTransform.forward * arrowHitMissDistance;
                bowScript.Fire(target, false);
            }
            StartCoroutine(Reload());
        }
    }
    private IEnumerator Reload() {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
    }
    void Update()
    {
        AimHandle();
        MovementHanle(); 
        aimTarget.position = cameraTransform.position + cameraTransform.forward * targetDistance;
        //read value input
        Vector2 input = moveAction.ReadValue<Vector2>();
        currentAnimationBlendVector = Vector2.SmoothDamp(currentAnimationBlendVector, input, ref animationVelocity, smoothTimeAnimation);
        Vector3 move = new Vector3(currentAnimationBlendVector.x, 0, currentAnimationBlendVector.y);
        //move character along with camera
        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);
        // Changes the height position of the player..
        if (jumpAction.triggered){
            animator.CrossFade(jumpAnimation,animationTransition);
        }

        animator.SetFloat(forwardParameterId,currentAnimationBlendVector.x);
        animator.SetFloat(strafeParameterId,currentAnimationBlendVector.y);
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        //rotate with the camera 
        Quaternion targetRotation = Quaternion.Euler(0,cameraTransform.eulerAngles.y,0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speedRotation * Time.deltaTime);
    }
    
 
    public void PullString()
    {
        bowScript.PullString();
    }

    public void EnableArrow()
    {
        bowScript.PickArrow();
    }

    public void DisableArrow()
    {
        bowScript.DisableArrow();
    }

    public void Release()
    {
        bowScript.ReleaseString();
    }

    public void PlayPullSound()
    {
        bowScript.PullAudio();
    }
}