using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterCtl : GameUnit
{
    public UnityAction OnDeathAction;
    public List<CharacterCtl> listEnemys;
    //vũ khí
    public Weapon currentWeapon;

    [SerializeField] private Transform pointShoot;

    [SerializeField] private Transform pointTarget;

    //Mục tiêu bắn 
    public CharacterCtl enemyTarget;
    private bool isMoving;

    public bool IsMoving { get => isMoving; set => isMoving = value; }

    [SerializeField] Animator anim;
    private string animName;

    private bool isAttack;
    private Coroutine attackCoroutine;

    private Dictionary<CharacterCtl, UnityAction> onDeathActions = new Dictionary<CharacterCtl, UnityAction>();


    protected virtual void Awake()
    {
        isAttack = false;
    }

    protected virtual void Start()
    {
        animName = Constants.ANIM_IDLE;
        ChangeAnim(Constants.ANIM_IDLE);
        listEnemys = new List<CharacterCtl>();
    }

    protected virtual void Update()
    {
        if(IsMoving == false && isAttack == false){
            ChangeAnim(Constants.ANIM_IDLE);
        }
        //nếu Enemy có trong list và ko di chuyển thì thực hiện bắn
        if (listEnemys.Count != 0)
        {
            if (!IsMoving && !isAttack)
            {
                attackCoroutine = StartCoroutine(CoFire());
            }
        }
        else if ((listEnemys.Count == 0) && attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
            isAttack = false;
        }
        if ((IsMoving == true) && attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
            isAttack = false;
        }
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
    private void FindEnemyTarget()
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
    IEnumerator CoFire()
    {
        isAttack = true;
        //tìm mục tiêu
        FindEnemyTarget();

        ChangeAnim(Constants.ANIM_ATTACK);

        RotateTowardsTarget();

        yield return new WaitForSeconds(1f);
        //bắn 
        RotateTowardsTarget();

        currentWeapon.Fire(enemyTarget);

        ChangeAnim(Constants.ANIM_IDLE);

        isAttack = false;
        attackCoroutine = null;
    }

    // xoay nhân vật theo hướng enemy
    private void RotateTowardsTarget()
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
}