using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterCtl : GameUnit
{
    [SerializeField] private Animator anim;
    [SerializeField] protected CharacterConfig characterConfig;
    [SerializeField] protected CapsuleCollider capsuleCollider;
    [SerializeField] private WeaponHolder weaponHolder;
    [SerializeField] protected GameObject targetObj;
    [SerializeField] protected Pant pant;

    private UnityAction OnDeathAction;
    public List<CharacterCtl> listEnemys = new List<CharacterCtl>();
    //vũ khí
    public Weapon CurrentWeapon { get; set; }
    public EWeapon ECurrentWeapon { get; set; }

    [SerializeField] protected AttackRange attackRange;
    [SerializeField] private Transform pointShoot;

    [SerializeField] private Transform pointTarget;

    //Mục tiêu bắn 
    public CharacterCtl enemyTarget;

    public bool IsMoving { get; set; }
    public bool IsAttack { get; set; }
    public float AttackSpeed { get; set; }
    public float MoveSpeed { get; set; }
    public float Range { get; set; }
    public bool IsDead { get ; set; }
    public WeaponHolder WeaponHolder => weaponHolder;

    private string animName = Constants.ANIM_IDLE;

    private Dictionary<CharacterCtl, UnityAction> onDeathActions = new Dictionary<CharacterCtl, UnityAction>();

    protected float prepareAttackDuration;
    protected float prepareAttackCounter;


    protected virtual void Awake()
    {
        IsAttack = false;
        
    }

    protected virtual void Start()
    {
        ChangeAnim(Constants.ANIM_IDLE);
        OnInit();
    }

    protected virtual void Update() {
    }

    public virtual void OnInit()
    {
        IsDead = false;
        SetInitialAttackRange();
        SetInitialAttackSpeed();
        SetInitialMoveSpeed();
    }

    // set tầm đánh
    public void SetInitialAttackRange()
    {
        if (attackRange != null)
        {
            Range = characterConfig.attackRange;
            attackRange.transform.localScale = new Vector3(Range, 0.1f, Range);
        }
    }
    public void AddAttackRange(float additionalRange)
    {
        Range += additionalRange;
        if (attackRange != null)
        {
            attackRange.transform.localScale = new Vector3(Range, 0.1f, Range);
        }
    }

    public void DelAttackRange(float additionalRange)
    {
        Range -= additionalRange;
        if (attackRange != null)
        {
            attackRange.transform.localScale = new Vector3(Range, 0.1f, Range);
        }
    }

    public void SetInitialAttackSpeed()
    {
        AttackSpeed = characterConfig.attackSpeed;
        prepareAttackDuration = 1 / AttackSpeed;
    }
    public void AddAttackSpeed(float additionalattackRange)
    {
        AttackSpeed += additionalattackRange;
        prepareAttackDuration = 1 / AttackSpeed;
    }
    public void DelAttackSpeed(float additionalattackRange)
    {
        AttackSpeed += additionalattackRange;
        prepareAttackDuration = 1 / AttackSpeed;
    }

    public void SetInitialMoveSpeed()
    {
        MoveSpeed = characterConfig.moveSpeed;
    }
    public void AddMoveSpeed(float additionalMoveSpeed)
    {
        MoveSpeed += additionalMoveSpeed;
    }
    public void DelMoveSpeed(float additionalMoveSpeed)
    {
        MoveSpeed += additionalMoveSpeed;
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
    protected virtual void FindEnemyTarget()
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
    public virtual void Die()
    {
        StartCoroutine(CoDead(2f));
    }
    // thực hiện xóa khỏi list, anim dead và đợi 1 khoảng thời gian thì xóa tự xóa đi 
    public IEnumerator CoDead(float time)
    {
        OnDeathAction?.Invoke();
        IsDead = true;
        capsuleCollider.enabled = false;
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

    public void setActiveTarget(bool isActive){
        if(targetObj == null) return;
        targetObj.gameObject.SetActive(isActive);
    }
}