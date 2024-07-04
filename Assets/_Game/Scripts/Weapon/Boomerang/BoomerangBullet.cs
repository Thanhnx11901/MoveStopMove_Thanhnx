using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoomerangBullet :  Bullet
{
    public float rotationSpeed = 1000f;
    [SerializeField]private float flightDistance;

    public float FlightDistance { get => flightDistance; set => flightDistance = value; }

    public override void OnInit(Vector3 direction, CharacterCtl owner, float timeDespawn)
    {
        rb.velocity = direction.normalized * speed;
        StartCoroutine(CoDespawn(timeDespawn));
        this.owner = owner;
    }

    IEnumerator CoDespawn(float timeDespawn){
        yield return new WaitForSeconds(timeDespawn);
        OnDespawn();
    }

    private void FixedUpdate()
    {
        
        // Quay cây búa quanh trục y
        rb.MoveRotation(rb.rotation * Quaternion.Euler(Vector3.up * rotationSpeed * Time.fixedDeltaTime));
        if(owner.IsDead) OnDespawn();
        if(Vector3.Distance(owner.TF.position, transform.position) > FlightDistance){
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
            OnDespawn();
            characterCtl.Die();

        }
    }
}

