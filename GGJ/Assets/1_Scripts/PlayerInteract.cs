using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour {

    private Inventory inventory;
    public Player player;

    public void Start() {
        inventory = GetComponent<Inventory>();

    }

    public void Update() {
    }

    public void OnTriggerStay2D(Collider2D other) {

        if(other.GetComponent<IMineable<float>>() != null) {
            IMineable<float> mine = other.GetComponent<IMineable<float>>();
            Debug.Log("minen maar!");
            Mine(mine);
        }

        if(other.GetComponent<IGrabable<Inventory>>() != null) {
            Debug.Log("graben maar!");
            IGrabable<Inventory> grab = other.GetComponent<IGrabable<Inventory>>();
            Grab(grab);
        }

        if(other.GetComponent<IInteractable>() != null) {
            IInteractable interact = other.GetComponent<IInteractable>();
            Interact(interact);
        }

    }

    public void Mine(IMineable<float> m) {

        if(Input.GetButtonDown(player.interactInput)) {
            m.TakeDamage(1);
        }

    }

    public void Grab(IGrabable<Inventory> g) {

        if(Input.GetButtonDown(player.interactInput)) {
            Debug.Log("grab!");
            g.Pickup(inventory);
        }
    }

    public void Interact(IInteractable i) {}

}