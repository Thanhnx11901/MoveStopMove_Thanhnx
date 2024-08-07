using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerCtl : CharacterCtl
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject gobjRange;
    private float horizontal;
    private float vertical;

    public VariableJoystick VariableJoystick { get; set; }

    protected override void Awake()
    {
        IsMoving = true;
        
    }
    protected override void Start()
    {
        base.Start();
    }
    public override void OnInit()
    {
        base.OnInit();
        rb.velocity = Vector3.zero;
        gameObject.SetActive(true);
        TF.position = Vector3.zero;
        TF.rotation = Quaternion.Euler(Vector3.up*180);
        //sinh vũ khí 
        EWeapon eWeapon = (EWeapon)PlayerPrefs.GetInt(Constants.CURRENT_WEAPON);
        // set vũ khí mặc định 
        WeaponHolder.ChangeWeapon(eWeapon);
        ChangeSkinPlayer.LoadSkin();
    }

    protected override void Update()
    {
        base.Update();
        if(GameManager.IsState(GameState.ShopSkin)) ChangeAnim(Constants.ANIM_DANCE);
        if(GameManager.IsState(GameState.MainMenu)) ChangeAnim(Constants.ANIM_IDLE);
        if(GameManager.IsState(GameState.Victory)) {
            ChangeAnim(Constants.ANIM_WIN);
            rb.velocity = Vector3.zero;
            TF.rotation = Quaternion.Euler(Vector3.up*180);
        }
        if(GameManager.IsState(GameState.GamePlay)) gobjRange.SetActive(true);
        else gobjRange.SetActive(false);
        Move();
    }
    private void Move()
    {
        if(VariableJoystick == null) return;
        if (!GameManager.IsState(GameState.GamePlay)){
            return;
        }
        if(IsDead) {
            rb.velocity = Vector3.zero;
            return;
        }
        if (IsMoving)
        {
            horizontal = VariableJoystick.Horizontal;
            vertical = VariableJoystick.Vertical;
        }
        if(Math.Abs(horizontal) < 0.00001f && Math.Abs(vertical) < 0.00001f){
            rb.velocity = Vector3.zero;
        }
        // khi player di chuyển
        if (IsPlayerMoving())
        {
            rb.velocity = new Vector3(horizontal * MoveSpeed, rb.velocity.y, vertical * MoveSpeed);

            if (rb.velocity != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(rb.velocity);
            }
        }
        // khi không di chuyển
        else
        {
            // check xem có bot nào ở trong tầm đánh ko 
            if (HaveCharacterInAttackRange())
            {
                //thực hiện đánh
                if (!IsAttack)
                {
                    AttackCharacterInRange();
                }
            }
        }
        if (!IsAttack && !IsPlayerMoving() && !IsDead)
        {
            ChangeAnim(Constants.ANIM_IDLE);
        }
        if(IsDead){
            prepareAttackCounter = 0;
        }
    }

    private void AttackCharacterInRange()
    {
        FindEnemyTarget();
        
        RotateTowardsTarget();
        prepareAttackCounter += Time.deltaTime;
        if (prepareAttackCounter >= prepareAttackDuration)
        {
            ChangeAnim(Constants.ANIM_ATTACK);
            // tan  cong
            StartCoroutine(CoFire());
            // them ham tan cong va trong khi tan cong thi khong di chuyen duoc 
            prepareAttackCounter = 0;
        }
    }

    private bool IsPlayerMoving()
    {
        if (Math.Abs(horizontal) > 0.00001f || Math.Abs(vertical) > 0.00001f)
        {
            prepareAttackCounter = 0;
            ChangeAnim(Constants.ANIM_RUN);
            return true;
        }
        else return false;
    }
    IEnumerator CoFire()
    {
        IsAttack = true;

        IsMoving = false;

        RotateTowardsTarget();
        
        yield return new WaitForSeconds(.2f);
        
        CurrentWeapon.ActiveMeshRenderer(false);
        CurrentWeapon.Fire(enemyTarget);        
        IsMoving = true;
    }

    protected override void Death()
    {
        gameObject.SetActive(false);
        GameManager.ChangeState(GameState.GameOver);
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<Lose>();
    }

    protected override void FindEnemyTarget()
    {
        if (listEnemys.Count == 0) return;

        float minDistance = float.MaxValue;

        for (int i = 0; i < listEnemys.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, listEnemys[i].transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                if(enemyTarget != null) enemyTarget.setActiveTarget(false);
                enemyTarget = listEnemys[i];
                enemyTarget.setActiveTarget(true);
            }
        }
    }

    public void IsActiveSkin(bool isActive){
        if(CurrentHair != null) CurrentHair.gameObject.SetActive(isActive);
        if(CurrentShield != null) CurrentShield.gameObject.SetActive(isActive);
    }
}
