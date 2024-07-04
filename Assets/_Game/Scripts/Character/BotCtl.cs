using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotCtl : CharacterCtl
{
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

    protected override void Start()
    {
        base.Start();
        ChangeState(IdleState);
    }
    public override void OnInit()
    {
        base.OnInit();
        //sinh vũ khí cho bot
        WeaponHolder.ChangeWeapon(EWeapon.Candy);
    }
    protected override void Update()
    {
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

    public void Move(Vector3 pos)
    {
        IsMoving = true;
        Agent.SetDestination(pos);
        

    }

    public Vector3 GetRandomPositionBot()
    {
        // Vector2 randomPoint2D = Random.insideUnitCircle * 15;
        // Vector3 randomPosition = new Vector3(randomPoint2D.x, 0, randomPoint2D.y) + TF.position;
        // return randomPosition;

        Vector3 randomDirection = Random.insideUnitSphere * 10f; 
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
        Debug.Log("attack");

        IsAttack = true;

        RotateTowardsTarget();

        yield return new WaitForSeconds(.2f);

        CurrentWeapon.ActiveMeshRenderer(false);
        CurrentWeapon.Fire(enemyTarget);

        //ChangeAnim(Constants.ANIM_IDLE);
    }
    public override void Die()
    {
        ChangeState(DieState);
        base.Die();
        LevelManager.Ins.currentLevel.removeBotWhenDead(this);
    }
}
