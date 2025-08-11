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

    public string GetInteractPrompot() // UI에 표시할 정보 (아이템 이름, 설명)
    {
        string str = $"{data.displayName}\n{data.description}";
        return str;
    }

    public void OnInteract()
    {
        throw new System.NotImplementedException();
    }
}
