using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")] // 이동 및 점프
    public float moveSpeed;
    private float OriginSpeed; // 기존 이동속도
    private Vector2 curMovementInput;
    public float jumpPower;
    public LayerMask groundLayerMask;
    public float uesStamina;

    [Header("Look")] // 시야
    public Transform cameraContainer;
    public float mixXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;

    [HideInInspector]
    public bool canLook = true;

    public Action inventory;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // 인벤토리 On/Off 시 마우스 고정이 반대로 발생하여 설정
        OriginSpeed = moveSpeed; // 최초 이동속도 저장
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    // ========================플레이어 이동============================

    public void OnMoveInput(InputAction.CallbackContext context) // 이동 신호 입력
    {
        if(context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void Move() // 이동
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = rb.velocity.y;

        rb.velocity = dir;
    }

    public void BoostSpeed(float amount, float duration) // 이동속도 증가
    {
        StartCoroutine(SpeedBoostCoroutine(amount, duration));
    }

    IEnumerator SpeedBoostCoroutine(float amount, float duration) // 이동속도 증감 코루틴
    {
        moveSpeed += amount;
        yield return new WaitForSeconds(duration);
        moveSpeed = OriginSpeed;
    }

    // ========================플레이어 점프============================

    public void OnJumpInput(InputAction.CallbackContext context) // 점프 신호 입력
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            if(CharacterManager.Instance.Player.condition.UseStamina(uesStamina))
            {
                rb.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
            }
        }    
    }

    bool IsGrounded() // 플레이어가 땅에 붙어있는지 여부 체크
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f),Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f),Vector3.down),
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f),Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f),Vector3.down),
        };

        for(int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    // ========================플레이어 시야============================

    public void OnLookInput(InputAction.CallbackContext context) // 시야 신호 입력
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    void CameraLook() // 카메라 움직임
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, mixXLook, maxXLook); // 시야각 범위 제한
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    // ========================플레이어 인벤토리============================

    public void OnInventoryInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            inventory?.Invoke();
            ToggleCursor();
        }
    }

    void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }
}
