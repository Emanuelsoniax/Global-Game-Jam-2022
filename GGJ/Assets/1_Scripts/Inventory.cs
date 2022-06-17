using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public event EventHandler OnItemListChanged;

    public List<Item> characterItems = new List<Item>();
    public Transform inventoryContainer;
    public int selectedItem;
    bool itemAlreadyInInventory = false;


    public void Start()
    {
        Debug.Log(characterItems);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RemoveItem(characterItems[selectedItem]);
            
        }

        Debug.Log(selectedItem);

    }

    public void AddItem(Item item)
    {
        if (item.IsStackable())
        {
            foreach (Item inventoryItem in characterItems)              //check of je het item al in je inventory hebt
            {
                if (inventoryItem.itemType == item.itemType)               //ja
                {
                    if(itemAlreadyInInventory){
                        inventoryItem.amount ++;
                    }
                    Destroy(item.transform.parent.gameObject);
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory)                                   //nee
            {
                characterItems.Add(item);
            }
        }
        else
        {
            characterItems.Add(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
        Debug.Log("Added item: " + item.id);

        if (characterItems.Count == 1)
        {
            selectedItem = 0;

        }
    }
    public void RemoveItem(Item item)
    {
        foreach (Item inventoryItem in characterItems)              //check of je het item al in je inventory hebt
        {
            if (inventoryItem.itemType == item.itemType)               //ja
            {
                inventoryItem.amount--;

                if (inventoryItem.amount == 0)
                {
                    itemAlreadyInInventory = false;
                    characterItems.Remove(inventoryItem);
                    Destroy(inventoryItem.gameObject);
                    PreviousItem();
                }
            }
            OnItemListChanged?.Invoke(this, EventArgs.Empty);
        }

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void NextItem()
    {
        characterItems[selectedItem].isSelected = false;
        selectedItem = (selectedItem + 1) % characterItems.Count;
        characterItems[selectedItem].isSelected = true;

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void PreviousItem()
    {
        characterItems[selectedItem].isSelected = false;
        selectedItem--;
        if (selectedItem < 0)
        {
            selectedItem += characterItems.Count;
        }

        characterItems[selectedItem].isSelected = true;

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }


    //public Item CheckForItem(int id)
    //{
    //    return characterItems.Find(item => item.GetComponent<Item>().item.id == id);
    //}

    //public void RemoveItem(GameObject item, int i)
    //{
    //    GameObject item = CheckForItem();
    //    if (item != null)
    //    {
    //        characterItems.Remove();
    //        Debug.Log("Item removed: " + item.title);
    //    }

    //}

}