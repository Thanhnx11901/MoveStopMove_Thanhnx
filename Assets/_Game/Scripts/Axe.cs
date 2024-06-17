using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : GameUnit
{
    private Vector3 current;
    private Vector3 target;
    [SerializeField] private float speed;

    public Vector3 Current { get => current; set => current = value; }
    public Vector3 Target { get => target; set => target = value; }

    private void FixedUpdate() {
         transform.position = Vector3.MoveTowards(transform.position, Target, speed * Time.fixedDeltaTime);
    }
}
