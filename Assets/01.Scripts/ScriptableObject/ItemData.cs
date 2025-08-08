using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Consumable,
    Equipable
}

public enum ConsumableType
{
    Health,
    Stamina
}

[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName; // 아이템 이름
    public string description; // 아이템 설명
    public ItemType type; // 아이템 타입
    public Sprite icon; // 아이템 아이콘
    public GameObject dropPrefab; // 필드 드롭 아이템 프리팹

    [Header("Stacking")]
    public bool canStack; // 소지 가능 여부
    public int maxStackAmount; // 최대 소지 가능 갯수

    [Header("Consumable")]
    public ItemDataConsumable[] consumables; // 소비 아이템 목록
}
