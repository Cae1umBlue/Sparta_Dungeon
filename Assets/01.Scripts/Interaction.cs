using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f; // 아이템 체크에 필요한 시간
    private float lastCheckTime; // 마지막 체크에 걸린 시간
    public float maxCheckDistance; // 최대 체크 거리
    public LayerMask layerMask;

    public GameObject curInteractionObject; // 현재 상호작용 아이템 오브젝트
    public IInteractable curInteractable; // 현재 상호작용 인터페이스

    public TextMeshProUGUI promptUI;
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }
}
