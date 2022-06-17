using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestInventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public Transform itemSlotContainer;
    public Transform itemSlotTemplate;

    [SerializeField]
    private float itemSlotCellSize = 30f;

    private void Awake()
    {
        inventory.OnItemListChanged += Inventory_OnItemListChanged;     //subcribe event
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        foreach(Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }
        
        int x = 0;
        int y = 0;

        foreach (Item item in inventory.characterItems)
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);
            //assign image
            Image image = itemSlotRectTransform.Find("image").GetComponent<Image>(); 
            image.sprite = item.sprite;
            //assign font
            TextMeshProUGUI text = itemSlotRectTransform.Find("text").GetComponent<TextMeshProUGUI>();
            text.SetText(item.amount.ToString());
            //assign selected
            Image selectedIm = itemSlotRectTransform.Find("selected").GetComponent<Image>();
            if(item.isSelected == true)
            {
                selectedIm.gameObject.SetActive(true);
            }

            //positioning
            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            x++;
            if(x > 3.5f)
            {
                x = 0;
                y--;
            }
        }
    }

}
