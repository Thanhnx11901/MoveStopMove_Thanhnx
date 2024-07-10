using System.Collections;
using UnityEngine;

public class Bullet : GameUnit
{
    [SerializeField] protected CharacterCtl owner;
    [SerializeField] protected Rigidbody rb;
    public float speed;

    public virtual void OnInit(Vector3 direction, CharacterCtl oner, float timeDespawn)
    {
        rb.velocity = direction.normalized * speed;
        StartCoroutine(CoDespawn(timeDespawn));
        this.owner = oner;
    }
    // hủy đạn khi sinh ra trong 1 khoảng thời gian 
    IEnumerator CoDespawn(float timeDespawn){
        yield return new WaitForSeconds(timeDespawn);
        OnDespawn();
    }

    protected void OnDespawn()
    {
        SimplePool.Despawn(this);
        owner.ResetAttack();
        owner.CurrentWeapon.ActiveMeshRenderer(true);   
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        CharacterCtl characterCtl = Cache<CharacterCtl>.GetCollider(other);
        if (characterCtl != null && characterCtl != owner) 
        {
            owner.AddAttackRange(0.01f);
            OnDespawn();
            characterCtl.Die();
        }
    }
}
