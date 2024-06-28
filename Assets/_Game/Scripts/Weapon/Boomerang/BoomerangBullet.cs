using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangBullet :  Bullet
{
    public float rotationSpeed = 1000f;
    private Vector3 direction;

    // public override void OnInit(Vector3 direction, CharacterCtl oner)
    // {
    //     rb.velocity = direction.normalized * speed;
    //     this.direction = direction;
    //     Invoke(nameof(OnDespawn), 2f);
    //     this.oner = oner;
    // }

    private void FixedUpdate()
    {
        // Quay cây búa quanh trục y
        rb.MoveRotation(rb.rotation * Quaternion.Euler(Vector3.up * rotationSpeed * Time.fixedDeltaTime));

        
        if(Vector3.Distance(owner.TF.position, transform.position) > 7f){
            Vector3 bulletDirection = owner.GetPointShoot() - transform.position;
            rb.velocity = bulletDirection.normalized * speed;
        }
        
    }

    protected override void OnTriggerEnter(Collider other)
    {
        CharacterCtl characterCtl = Cache<CharacterCtl>.GetCollider(other);
        if (characterCtl != null && characterCtl != owner) 
        {
             Vector3 bulletDirection = owner.GetPointShoot() - transform.position;
             rb.velocity = bulletDirection.normalized * speed;
            //OnDespawn();
            //characterCtl.Die();

        }
    }
}

