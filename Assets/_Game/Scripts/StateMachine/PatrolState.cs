using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<BotCtl>
{
    private Vector3 destination;

    float timer;
    float moveTime;

    public void OnEnter(BotCtl t)
    {
        timer = 0;
        moveTime = 5f;
        destination = t.GetRandomPositionBot();
        t.Move(destination);
        t.ChangeAnim(Constants.ANIM_RUN);
    }

    public void OnExecute(BotCtl t)
    {
        if(Vector3.Distance(destination, t.TF.position) < 0.1f || timer > moveTime){
            t.ChangeState(t.IdleState);
        }
        timer += Time.deltaTime; 

    }

    public void OnExit(BotCtl t)
    {
        t.IsMoving = false;
    }

}
