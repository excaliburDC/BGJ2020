using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractor : MonoBehaviour
{

    public float gravity = -10f;


    public void Attract(Rigidbody body)
    {
        
        Vector3 targetDir = (body.position - transform.position).normalized;
        Vector3 bodyUp = body.transform.up;

        // Apply downwards gravity to body
        body.AddForce(targetDir * gravity);
        // Allign bodies up axis with the centre of planet
        body.rotation = Quaternion.FromToRotation(bodyUp, targetDir) * body.rotation;
       
    }
}
