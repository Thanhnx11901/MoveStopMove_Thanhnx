using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeBullet : Bullet
{
    public float rotationSpeed = 1000f;

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.LookRotation(rb.velocity);
        //rb.MoveRotation(rb.rotation * Quaternion.Euler(Vector3.up * rotationSpeed * Time.fixedDeltaTime));
    }
}
