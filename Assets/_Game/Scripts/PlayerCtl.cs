using System;
using UnityEditor;
using UnityEngine;

public class PlayerCtl : CharacterCtl
{
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float speed;

    private float horizontal;
    private float vertical;

    protected override void Update()
    {
        move();
        base.Update();
    }
    private void move()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (Math.Abs(horizontal) > 0.00001f || Math.Abs(vertical) > 0.00001f)
        {
            IsMoving = true;
            ChangeAnim(Constants.ANIM_RUN);
        }
        else
        {
            IsMoving = false;
        }

        rb.velocity = new Vector3(horizontal * speed, rb.velocity.y, vertical * speed);

        if (rb.velocity != Vector3.zero){
            transform.rotation = Quaternion.LookRotation(rb.velocity);
        }

    }

}
