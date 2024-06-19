using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public CharacterCtl characterCtlOner;

    private void OnTriggerEnter(Collider other)
    {
        CharacterCtl enemy = Cache<CharacterCtl>.GetCollider(other);
        if (enemy != null)
        {
            // thêm vào Enemy vào list và thêm a
            characterCtlOner.AddListEnemy(enemy);
            characterCtlOner.AddEnemyDeadAction(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CharacterCtl characterCtl = Cache<CharacterCtl>.GetCollider(other);
        if (characterCtl != null)
        {
            characterCtlOner.RemoveEnemyFromList(characterCtl);
            characterCtlOner.RemoveEnemyDeadAction(characterCtl);
        }
    }
}
