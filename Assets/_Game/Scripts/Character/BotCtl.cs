using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotCtl : CharacterCtl
{
    [SerializeField] private Target target;
    [SerializeField] private NavMeshAgent agent;
    private IState<BotCtl> currentState;
    private IdleState idleState = new IdleState();
    private PatrolState patrolState = new PatrolState();
    private AttackState attackState = new AttackState();
    private DieState dieState = new DieState();

    public NavMeshAgent Agent => agent;
    public IdleState IdleState { get => idleState; set => idleState = value; }
    public PatrolState PatrolState { get => patrolState; set => patrolState = value; }
    public AttackState AttackState { get => attackState; set => attackState = value; }
    public DieState DieState { get => dieState; set => dieState = value; }
    [SerializeField] private ChangeSkinBot changeSkinBot;
    protected override void Start()
    {
        base.Start();
        ChangeState(IdleState);
    }
    public override void OnInit()
    {
        base.OnInit();
        //sinh vũ khí cho bot

        WeaponHolder.ChangeWeaponBot(GetRandomEnumValue<EWeapon>());

        changeSkinBot.LoadSkinRandom();
        target.TargetColor = changeSkinBot.skinnedMeshRenderer.material.color;
    }
    protected override void Update()
    {
        if(GameManager.IsState(GameState.GamePlay)) agent.enabled = true;
        else agent.enabled = false;
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
        if (!IsAttack && !IsMoving && !IsDead)
        {
            ChangeAnim(Constants.ANIM_IDLE);
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
    public override void SetInitialMoveSpeed()
    {
        MoveSpeed = GameData.Ins.characterConfig.moveSpeed;
        agent.speed = MoveSpeed;
    }
    public override void AddMoveSpeed(float additionalMoveSpeed)
    {
        MoveSpeed += additionalMoveSpeed;
        agent.speed = MoveSpeed;
    }

    public void Move(Vector3 pos)
    {
        IsMoving = true;
        Agent.SetDestination(pos);
    }

    public Vector3 GetRandomPositionBot()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * 10f;
        randomDirection += TF.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, 10f, NavMesh.AllAreas);
        return hit.position;
    }
    public void AttackCharacterInRange()
    {
        FindEnemyTarget();

        RotateTowardsTarget();
        prepareAttackCounter += Time.deltaTime;
        if (prepareAttackCounter >= prepareAttackDuration)
        {
            ChangeAnim(Constants.ANIM_ATTACK);
            // tan  cong
            StartCoroutine(CoFire());
            prepareAttackCounter = 0;
        }
    }
    IEnumerator CoFire()
    {
        IsAttack = true;

        RotateTowardsTarget();

        yield return new WaitForSeconds(.2f);

        CurrentWeapon.ActiveMeshRenderer(false);
        CurrentWeapon.Fire(enemyTarget);

    }
    public override void Die()
    {
        ChangeState(DieState);
        base.Die();
        LevelManager.Ins.currentLevel.removeBotWhenDead(this);
    }
    protected override void Death()
    {
        SimplePool.Despawn(this);
    }

    public T GetRandomEnumValue<T>() where T : Enum
    {
        T[] values = (T[])Enum.GetValues(typeof(T));
        int randomIndex = UnityEngine.Random.Range(0, values.Length);
        return values[randomIndex];
    }
}
