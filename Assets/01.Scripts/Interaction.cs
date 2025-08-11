using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f; // 아이템 체크에 필요한 시간
    private float lastCheckTime; // 마지막 체크에 걸린 시간
    public float maxCheckDistance; // 최대 체크 거리
    public LayerMask layerMask;

    public GameObject curInteractionObject; // 현재 상호작용 아이템 오브젝트
    public IInteractable curInteractable; // 현재 상호작용 인터페이스

    public TextMeshProUGUI promptText;
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        if(Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != curInteractionObject)
                {
                    curInteractionObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();
                }
            }
            else
            {
                curInteractionObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable.GetInteractPrompot();
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        // 인풋 입력의 현재 상태 체크 및 상호작용 인터페이스 여부 확인
        if(context.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnInteract();
            curInteractionObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }
}
