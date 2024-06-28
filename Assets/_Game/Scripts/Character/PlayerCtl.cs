using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerCtl : CharacterCtl
{
    [SerializeField] private Rigidbody rb;

    private float horizontal;
    private float vertical;

    protected override void Awake()
    {
        IsMoving = true;
        
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void OnInit()
    {
        base.OnInit();
    }

    private void Update()
    {
        Move();
    }
    private void Move()
    {
        if (IsMoving)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
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
        if (!IsAttack && !IsPlayerMoving())
        {
            ChangeAnim(Constants.ANIM_IDLE);
        }
    }

    private void AttackCharacterInRange()
    {
        FindEnemyTarget();
        ChangeAnim(Constants.ANIM_ATTACK);
        RotateTowardsTarget();
        prepareAttackCounter += Time.deltaTime;
        if (prepareAttackCounter >= prepareAttackDuration)
        {
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
        Debug.Log("attack");

        IsAttack = true;

        IsMoving = false;

        RotateTowardsTarget();
        
        yield return new WaitForSeconds(.2f);
        
        currentWeapon.ActiveMeshRenderer(false);
        currentWeapon.Fire(enemyTarget);
        
        //ChangeAnim(Constants.ANIM_IDLE);

        IsMoving = true;
    }
}
