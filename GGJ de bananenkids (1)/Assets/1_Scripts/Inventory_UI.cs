using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Inventory_UI : MonoBehaviour {

    public Canvas inventoryCanvas;
    public Player player;

    private bool inventoryEnabled = false;

    public void Start() {
    }

    public void Update() {

        inventoryCanvas.enabled = inventoryEnabled;

        transform.position = player.transform.position;

        if(Input.GetButtonDown(player.inventoryInput)) {
            inventoryEnabled = !inventoryEnabled;
        }

    }

}