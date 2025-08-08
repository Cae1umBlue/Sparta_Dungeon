using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")] // �̵� �� ����
    public float moveSpeed;
    private Vector2 curMovementInput;
    public float jumpPower;
    public LayerMask groundLayerMask;

    [Header("Look")] // �þ�
    public Transform cameraContainer;
    public float mixXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;

    [HideInInspector]
    public bool canLook = true;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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

    // ========================�÷��̾� �̵�============================

    public void OnMoveInput(InputAction.CallbackContext context) // �̵� ��ȣ �Է�
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

    public void Move() // �̵�
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = rb.velocity.y;

        rb.velocity = dir;
    }

    // ========================�÷��̾� ����============================

    public void OnJumpInput(InputAction.CallbackContext context) // ���� ��ȣ �Է�
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }    
    }

    bool IsGrounded() // �÷��̾ ���� �پ��ִ��� ���� üũ
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

    // ========================�÷��̾� �þ�============================

    public void OnLookInput(InputAction.CallbackContext context) // �þ� ��ȣ �Է�
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    void CameraLook() // ī�޶� ������
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, mixXLook, maxXLook); // �þ߰� ���� ����
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }
}
