using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterCtl : GameUnit
{
    [SerializeField] protected CharacterConfig characterConfig;
    [SerializeField] protected WeaponHolder weaponHolder;

    [SerializeField] protected Pant pant;

    private UnityAction OnDeathAction;
    public List<CharacterCtl> listEnemys;
    //vũ khí
    public Weapon currentWeapon;

    [SerializeField] protected AttackRange attackRange;
    [SerializeField] private Transform pointShoot;

    [SerializeField] private Transform pointTarget;

    //Mục tiêu bắn 
    public CharacterCtl enemyTarget;
    private bool isMoving;

    public bool IsMoving { get => isMoving; set => isMoving = value; }
    public bool IsAttack { get => isAttack; set => isAttack = value; }
    public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public float Range { get => Range; set => Range = value; }

    [SerializeField] Animator anim;
    private string animName;

    private bool isAttack;

    private Dictionary<CharacterCtl, UnityAction> onDeathActions = new Dictionary<CharacterCtl, UnityAction>();

    protected float prepareAttackDuration;
    protected float prepareAttackCounter;


    protected virtual void Awake()
    {
        IsAttack = false;
    }

    protected virtual void Start()
    {
        animName = Constants.ANIM_IDLE;
        ChangeAnim(Constants.ANIM_IDLE);
        listEnemys = new List<CharacterCtl>();
        OnInit();
    }

    // tầm đánh
    [SerializeField] private float range;
    // tốc độ đánh 
    [SerializeField] private float attackSpeed;
    // tốc độ di chuyển
    [SerializeField] private float moveSpeed;

    // set tầm đánh
    public void SetInitialAttackRange()
    {
        if (attackRange != null)
        {
            range = characterConfig.attackRange;
            attackRange.transform.localScale = new Vector3(range, 0.1f, range);
        }
    }
    public void AddAttackRange(float additionalRange)
    {
        range += additionalRange;
        if (attackRange != null)
        {
            attackRange.transform.localScale = new Vector3(range, 0.1f, range);
        }
    }

    public void SetInitialAttackSpeed(){
        attackSpeed = characterConfig.attackSpeed;
        prepareAttackDuration = 1/ attackSpeed;
    }
    public void AddAttackSpeed(float additionalattackRange){
        attackSpeed += additionalattackRange;
        prepareAttackDuration = 1/ attackSpeed;
    }

    public void SetInitialMoveSpeed(){
        moveSpeed = characterConfig.moveSpeed;
    }
    public void AddMoveSpeed(float additionalMoveSpeed){
        MoveSpeed += additionalMoveSpeed;
    }


    protected virtual void OnInit()
    {
        SetInitialAttackRange();
        SetInitialAttackSpeed();
        SetInitialMoveSpeed();
        // sinh vũ khí 
        weaponHolder.SpawnWeapon();
        // Các thiết lập khác trong OnInit()
    }










    public void ChangeAnim(string animName)
    {
        if (this.animName != animName)
        {
            anim.ResetTrigger(this.animName);
            this.animName = animName;
            anim.SetTrigger(this.animName);
        }
    }

    // thêm enemy vào list 
    public void AddListEnemy(CharacterCtl enemy)
    {
        if (listEnemys.Contains(enemy)) return;
        listEnemys.Add(enemy);
    }
    // xóa enemy khỏi list
    public void RemoveEnemyFromList(CharacterCtl enemy)
    {
        if (listEnemys.Contains(enemy))
        {
            listEnemys.Remove(enemy);
        }
    }
    // thêm sự kiện khi enemy chết thì xóa khỏi list
    public void AddEnemyDeadAction(CharacterCtl enemy)
    {
        UnityAction action = () => RemoveEnemyFromList(enemy);
        enemy.OnDeathAction += action;
        // Store the reference to the action
        onDeathActions[enemy] = action;
    }
    public void RemoveEnemyDeadAction(CharacterCtl enemy)
    {
        // tìm kiểu trong dict có enemy ko , nếu có thì trả về action của có vào biến action 
        if (onDeathActions.TryGetValue(enemy, out UnityAction action))
        {
            enemy.OnDeathAction -= action;
            onDeathActions.Remove(enemy);
        }
    }


    //tìm mục tiêu gần nhất
    protected void FindEnemyTarget()
    {
        if (listEnemys.Count == 0) return;

        float minDistance = float.MaxValue;

        for (int i = 0; i < listEnemys.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, listEnemys[i].transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                enemyTarget = listEnemys[i];
            }
        }

    }
    // hàm thực hiện bắn 

    public void ResetAttack()
    {
        IsAttack = false;
    }

    // xoay nhân vật theo hướng enemy
    protected void RotateTowardsTarget()
    {
        Vector3 direction = (enemyTarget.TF.position - TF.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        TF.rotation = lookRotation;
    }
    // chết thì gọi hàm này
    public void Die()
    {
        StartCoroutine(CoDead(2f));
    }
    // thực hiện xóa khỏi list, anim dead và đợi 1 khoảng thời gian thì xóa tự xóa đi 
    IEnumerator CoDead(float time)
    {
        OnDeathAction?.Invoke();
        ChangeAnim(Constants.ANIM_DEAD);
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
    // lấy v
    public Vector3 GetPointShoot()
    {
        return pointShoot.transform.position;
    }
    public Vector3 GetPointTarget()
    {
        return pointTarget.transform.position;
    }

    public bool HaveCharacterInAttackRange() => listEnemys.Count > 0;
}