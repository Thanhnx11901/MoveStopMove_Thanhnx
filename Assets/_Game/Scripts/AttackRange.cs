using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public CharacterCtl characterCtlOwner;

    private void OnTriggerEnter(Collider other)
    {
        CharacterCtl enemy = Cache<CharacterCtl>.GetCollider(other);
        if (enemy != null && enemy != characterCtlOwner)
        {
            // thêm vào Enemy vào list và thêm a
            characterCtlOwner.AddListEnemy(enemy);
            characterCtlOwner.AddEnemyDeadAction(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CharacterCtl enemy = Cache<CharacterCtl>.GetCollider(other);
        if (enemy != null && enemy != characterCtlOwner)
        {
            characterCtlOwner.RemoveEnemyFromList(enemy);
            characterCtlOwner.RemoveEnemyDeadAction(enemy);
        }
    }
}
