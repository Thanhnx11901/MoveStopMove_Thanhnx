using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeBullet : Bullet
{
    public float rotationSpeed = 1000f;

    private void FixedUpdate()
    {
        // Quay cây búa quanh trục y
        rb.MoveRotation(rb.rotation * Quaternion.Euler(Vector3.up * rotationSpeed * Time.fixedDeltaTime));
    }
}
