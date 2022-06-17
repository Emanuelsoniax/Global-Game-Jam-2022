using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    const float G = 66.74f;
    public Rigidbody2D rb;

    [Range(0.0f, 200.0f)]
    public float mass = 100f;
    private float rMass;

    public float rad;

    public List<Attractable> Attractables;


    private void FixedUpdate()
    {
        foreach( Attractable attractable in Attractables)
        {
          Attract(attractable);
        }
    }


    private void OnEnable()
    {
        float sizeMod = transform.localScale.x/10;
        rMass = mass*sizeMod;
        rb.mass = mass * 2;
    }


    public void Attract (Attractable objectToAttract)
    {
        if (objectToAttract == null)
        {
            Attractables.Remove(objectToAttract);
        }
        
        Rigidbody2D rbToAttract = objectToAttract.rb;

        //get direction
        Vector2 direction = rb.position - rbToAttract.position;
        float distance = direction.magnitude;                           //length of the direction vector

        if(distance <= rad) {

            float forceMagnitude = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
            Vector2 force = direction.normalized * forceMagnitude;

            rbToAttract.AddForce(force);
            objectToAttract.transform.rotation = Quaternion.FromToRotation(-objectToAttract.transform.up, direction) * objectToAttract.transform.rotation;
        }  
    }

    void OnDrawGizmosSelected(){
        Gizmos.DrawWireSphere(transform.position, rad);
    }
}
