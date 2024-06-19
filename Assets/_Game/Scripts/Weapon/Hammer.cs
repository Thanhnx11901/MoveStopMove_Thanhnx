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
        bullet.TF.position = oner.GetPointShoot();
        Vector3 bulletDirection = enemy.GetPointShoot() - oner.GetPointShoot();
        bullet.OnInit(bulletDirection,oner);
    }
}
