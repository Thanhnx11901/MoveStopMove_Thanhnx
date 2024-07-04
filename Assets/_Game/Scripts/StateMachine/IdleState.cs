using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<BotCtl>
{
    float timer;
    float randomTime;
    public void OnEnter(BotCtl t)
    {
        timer = 0;

        randomTime = Random.Range(0.5f, 6f);

        t.ChangeAnim(Constants.ANIM_IDLE);
    }

    public void OnExecute(BotCtl t)
    {
        if (!GameManager.IsState(GameState.GamePlay)) return;
        if(timer > randomTime){
            t.ChangeState(t.PatrolState);
        }
        timer += Time.deltaTime; 

        if(t.HaveCharacterInAttackRange()){
            t.ChangeState(t.AttackState);
        }
    }

    public void OnExit(BotCtl t)
    {

    }

}
