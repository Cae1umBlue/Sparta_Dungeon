using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData item;

    public UIInventory inventory;
    public Image Icon;
    public TextMeshProUGUI quatityText;
    private Outline outline;

    public int index;
    public int quantity;
}
