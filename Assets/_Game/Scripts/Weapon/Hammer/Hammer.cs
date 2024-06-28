using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Weapon
{
    public override void Fire(CharacterCtl enemy)
    {
        base.Fire(enemy);
        Bullet bullet = SimplePool.Spawn<HammerBullet>(PoolType.Hammer);
        bullet.TF.position = Owner.GetPointShoot();
        Vector3 bulletDirection = enemy.GetPointShoot() - Owner.GetPointShoot();
        bullet.OnInit(bulletDirection,Owner,TimeDespawn);
    }
}
