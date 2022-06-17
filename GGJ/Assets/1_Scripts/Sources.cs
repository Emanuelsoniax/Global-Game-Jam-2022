using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sources : MonoBehaviour {

    public float timerLength = 6f;

    private List<GameObject> source = new List<GameObject>();

    public void SourceControl(GameObject s) {

        source.Add(s);
        source[source.Count-1].SetActive(false);
        Invoke("Reload", timerLength);
        
    }

    public void Reload() {

        source[source.Count-1].SetActive(true);
        source.Remove(source[source.Count-1]);

    }

}