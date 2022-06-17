using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Source : MonoBehaviour, IMineable<float> {

    public int Health {get; set;}
    public int health = 3;

    public List<GameObject> drops;

    public Sources sourceContainer;

    public enum sources {

        Tree,
        Rock,
        Water
        
    }

    public sources source;

    public void Awake() {
        Health = health;
    }

    public void Start() {

        sourceContainer = FindObjectOfType<Sources>();

    }

    public void TakeDamage(float dmg) {
        Health -= 1;

        if(Health == 0) {
            Action();
        }

    }

    public void Action() {

        GameObject copy = Instantiate(this.gameObject, transform .position, transform.rotation);
        sourceContainer.SourceControl(copy);

        switch(source) {

            case sources.Tree:
                Instantiate(drops[UnityEngine.Random.Range(0, drops.Count)], transform.position, Quaternion.identity);
                Destroy(this.gameObject);
                break;
            case sources.Rock:
                Instantiate(drops[UnityEngine.Random.Range(0, drops.Count)], transform.position, Quaternion.identity);
                Destroy(this.gameObject);
                break;
            case sources.Water:
                Instantiate(drops[UnityEngine.Random.Range(0, drops.Count)], transform.position, Quaternion.identity);
                Destroy(this.gameObject);
                break;
            default:
                Debug.Log("Nada");
                break;
        
        }
    }
    
}