using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameUnit
{
    [SerializeField] protected CharacterCtl oner;
    [SerializeField] protected Rigidbody rb;
    public float speed;

    public void OnInit(Vector3 direction, CharacterCtl oner)
    {
        rb.velocity = direction.normalized * speed;
        Invoke(nameof(OnDespawn), 2f);
        this.oner = oner;
    }

    protected void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    protected void OnTriggerEnter(Collider other)
    {
        CharacterCtl characterCtl = Cache<CharacterCtl>.GetCollider(other);
        if (characterCtl != null && characterCtl != oner) 
        {
            OnDespawn();
            //characterCtl.Die();
        }
    }
}
