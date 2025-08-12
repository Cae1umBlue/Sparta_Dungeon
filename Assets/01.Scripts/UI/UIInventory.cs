using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] slots;

    public GameObject inventoryWindow;
    public Transform slotPanel;
    public Transform dropPosition;

    [Header("Selected Item")]
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI selectedItemStatName;
    public TextMeshProUGUI selectedItemStatValue;
    public GameObject useButton;
    public GameObject dropButton;
    public Image selectedItemIcon;

    private ItemData selectedItem;
    private int selectedItemIndex = 0;

    private PlayerController controller;
    private PlayerCondition condition;

    private void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
        condition = CharacterManager.Instance.Player.condition;
        dropPosition = CharacterManager.Instance.Player.dropPosition;

        controller.inventory += Toggle;
        CharacterManager.Instance.Player.addItem += AddItem;

        inventoryWindow.SetActive(false);
        slots = new ItemSlot[slotPanel.childCount];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].inventory = this;
        }

        ClearSelectedItemWindows();
    }

    // ======================== 인벤토리 UI 관리 ============================


    void ClearSelectedItemWindows() // 선택한 아이템 정보창 초기화
    {
        selectedItem = null;

        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
        selectedItemStatName.text = string.Empty;
        selectedItemStatValue.text = string.Empty;

        useButton.SetActive(false);
        dropButton.SetActive(false);

        selectedItemIcon.sprite = null; // 스프라이트가 아니라 이미지 컴포넌트 자체를 초기화 시켜서 문제가 됬다
    }

    void Toggle() // 인벤토리 창 on/off
    {
        if (IsOpen())
        {
            inventoryWindow.SetActive(false);
        }
        else
        {
            inventoryWindow.SetActive(true);
        }
    }

    bool IsOpen() // 인벤토리창 ON/OFF 체크
    {
        return inventoryWindow.activeInHierarchy;
    }

    public void SelectItem(int index) // 아이템 선택 - 기능분리 필요(리팩토링 해보자)
    {
        //Debug.Log(selectedItemIcon == null ? "IconImage is NULL" : "IconImage OK", this);
        //Debug.Log(selectedItem == null ? "SelectedItem is NULL" : "SelectedItem OK", this);


        if (slots[index].item == null) return;

        selectedItem = slots[index].item;
        selectedItemIndex = index;

        selectedItemIcon.sprite = selectedItem.icon;

        selectedItemName.text = selectedItem.displayName;
        selectedItemDescription.text = selectedItem.description;

        selectedItemStatName.text = string.Empty;
        selectedItemStatValue.text = string.Empty;

        for (int i = 0; i < selectedItem.consumables.Length; i++)
        {
            selectedItemStatName.text += selectedItem.consumables[i].type.ToString() + "\n";
            selectedItemStatValue.text += selectedItem.consumables[i].value.ToString() + "\n";
        }

        useButton.SetActive(selectedItem.type == ItemType.Consumable);
        dropButton.SetActive(true);
    }

    ItemSlot GetItemStack(ItemData data) // 아이템 소지 수량
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == data && slots[i].quantity < data.maxStackAmount)
            {
                return slots[i];
            }
        }
        return null;
    }

    void UpdateUI() // UI 새로고침
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                slots[i].Set();
            }
            else
            {
                slots[i].Clear();
            }
        }
    }

    // ======================== 인벤토리 데이터 관리 ============================


    void AddItem() // 아이템 추가
    {
        ItemData data = CharacterManager.Instance.Player.itemData;

        if (data.canStack) // 아이템 중복 소지 가능시
        {
            ItemSlot slot = GetItemStack(data);
            if (slot != null)
            {
                slot.quantity++;
                UpdateUI();
                CharacterManager.Instance.Player.itemData = null;
                return;
            }
        }

        ItemSlot emptySlot = GetEmptySlot(); // 빈 슬롯 찾기

        if (emptySlot != null) // 빈 슬롯이 있다면
        {
            emptySlot.item = data;
            emptySlot.quantity = 1;
            UpdateUI();
            CharacterManager.Instance.Player.itemData = null;
            return;
        }

        ThrowItem(data); // 빈슬롯이 없다면
        CharacterManager.Instance.Player.itemData = null;
    }

    ItemSlot GetEmptySlot() // 빈 슬롯 탐색
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                return slots[i];
            }
        }
        return null;
    }

    void RemoveSelectedItem() // 선택 또는 사용한 아이템 제거
    {
        slots[selectedItemIndex].quantity--;

        if (slots[selectedItemIndex].quantity <= 0)
        {
            selectedItem = null;
            slots[selectedItemIndex].item = null;
            selectedItemIndex = -1;
            ClearSelectedItemWindows();
        }

        UpdateUI();
    }


    // ======================== 인벤토리 아이템 조작 기능 ============================


    public void OnUseButton() // 아이템 사용 버튼
    {
        if (selectedItem.type == ItemType.Consumable)
        {
            for (int i = 0; i < selectedItem.consumables.Length; i++)
            {
                switch (selectedItem.consumables[i].type)
                {
                    case ConsumableType.Health:
                        condition.Heal(selectedItem.consumables[i].value); break;
                    case ConsumableType.Stamina:
                        condition.Heal(selectedItem.consumables[i].value); break; // 스테미나 전용 메서드 만들어야함
                }
            }
            RemoveSelectedItem();
        }
    }

    public void OnDropButton() // 아이템 버리기 버튼
    {
        ThrowItem(selectedItem);
        RemoveSelectedItem();
    }

    public void ThrowItem(ItemData data) // 아이템 버리기
    {
        Instantiate(data.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360));
    }
}
