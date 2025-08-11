using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] slots;

    public GameObject inventoryWindow;
    public Transform slotPanel;
    public Transform dropPosition;

    [Header("Selected Item")]
    private ItemSlot selectedItem;
    private int selectedItemIndex;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI selectedItemStatName;
    public TextMeshProUGUI selectedItemStatvalue;
    public GameObject useButton;
    public GameObject dropButton;

    private int curEqiuipIndex;

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

        for(int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].inventory = this;
            slots[i].Clear();
        }

        ClearSelectedItemWindows();
    }

    void ClearSelectedItemWindows()
    {
        selectedItem = null;

        selectedItemName.text = string.Empty;
        selectedItemDescription.text  = string.Empty;
        selectedItemStatName.text = string.Empty;
        selectedItemStatvalue.text = string.Empty;

        useButton.SetActive(false);
        dropButton.SetActive(false);
    }

    void Toggle()
    {

    }

    void AddItem()
    {

    }
}
