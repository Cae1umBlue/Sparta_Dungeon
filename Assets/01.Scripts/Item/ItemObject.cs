using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IInteractable
{
    public string GetInteractPrompot(); // UI에 표시할 정보
    public void OnInteract(); // 상호작용 호출
}
public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;

    public string GetInteractPrompot()
    {
        throw new System.NotImplementedException();
    }

    public void OnInteract()
    {
        throw new System.NotImplementedException();
    }
}
