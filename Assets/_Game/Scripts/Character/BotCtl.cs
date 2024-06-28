using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCtl : CharacterCtl
{
    private IState<BotCtl> currentState;
    
    protected override void Start()
    {
        //base.Start();
        ChangeState(new IdleState());
    }
    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }

    public void ChangeState(IState<BotCtl> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    
}
