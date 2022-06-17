using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractable : MonoBehaviour
{
    public Rigidbody2D rb;
    public float mass;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Attractor[] _attractors = FindObjectsOfType<Attractor>();
        foreach (Attractor attractor in _attractors)
        {
            attractor.Attractables.Add(this);
        }

    }

    private void OnEnable()
    {
        rb.mass = mass;
    }
}
