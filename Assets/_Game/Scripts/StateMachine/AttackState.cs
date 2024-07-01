using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<BotCtl>
{
    public void OnEnter(BotCtl t)
    {

    }

    public void OnExecute(BotCtl t)
    {
        if (!t.IsAttack)
        {
            t.AttackCharacterInRange();
        }
        if (!t.HaveCharacterInAttackRange())
        {
            t.ChangeState(t.IdleState);
        }
    }

    public void OnExit(BotCtl t)
    {

    }

}
