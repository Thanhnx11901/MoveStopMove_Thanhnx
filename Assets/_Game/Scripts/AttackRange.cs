using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public PlayerCtl playerCtl;
    private void OnTriggerEnter(Collider other)
    {
        BotCtl botCtl = Cache<BotCtl>.GetCollider(other);
        if (botCtl != null)
        {
            Axe axe = SimplePool.Spawn<Axe>(PoolType.Axe, transform);
            axe.Current = playerCtl.transform.position;
            axe.Target = botCtl.transform.position;
            axe.transform.localScale = new Vector3(.3f, .3f, .3f);
        }
    }
}
