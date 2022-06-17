using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour ,IGrabable<Inventory> {

    public enum items {

        Stick,
        Plank,
        Flint,
        IronOre,
        Iron,
        Nail,
        Clay,
        Stone
    }

    public int id;
    public string title;

    public int amount;
    public Sprite sprite;
    public GameObject prefab;
    public items itemType;
    public bool isSelected = false;

    public void Start() {
        SetItem();
    }

    public void Pickup(Inventory inventory) {

        GameObject item = Instantiate(prefab, inventory.inventoryContainer);
        inventory.AddItem(item.GetComponentInChildren<Item>());
        item.SetActive(false);

        Destroy(transform.parent.gameObject);
    }

    public void SetItem() {

        switch(itemType) {

            case items.Flint:
                id = 0;
                title = "Flint";
                amount = 1;
                break;
            
            case items.Clay:
                id = 1;
                title = "Clay";
                amount = 1;
                break;
            
            case items.Iron:
                id = 2;
                title = "Iron";
                amount = 1;
                break;

            case items.IronOre:
                id = 3;
                title = "Iron";
                amount = 1;
                break;

            case items.Nail:
                id = 4;
                title = "Nail";
                amount = 1;
                break;

            case items.Plank:
                id = 5;
                title = "Plank";
                amount = 1;
                break;

            case items.Stick:
                id = 6;
                title = "Stick";
                amount = 1;
                break;

            case items.Stone:
                id = 7;
                title = "Stone";
                amount = 1;
                break;
        }
    }

    public bool IsStackable()
    {
        switch (itemType)
        {
            default:
            case items.Clay:
            case items.Flint:
            case items.Iron:
            case items.IronOre:
            case items.Nail:
            case items.Plank:
            case items.Stick:
            case items.Stone:
                return true;
        }
    }

}