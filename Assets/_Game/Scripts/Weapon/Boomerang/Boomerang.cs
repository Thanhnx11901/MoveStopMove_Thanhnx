using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang :  Weapon
{
    public override void Fire(CharacterCtl enemy)
    {
        Bullet bullet = SimplePool.Spawn<BoomerangBullet>(PoolType.Boomerang);
        bullet.TF.position = Owner.GetPointShoot();
        Vector3 bulletDirection = enemy.GetPointShoot() - Owner.GetPointShoot();
        bullet.OnInit(bulletDirection,Owner,TimeDespawn);
    }
}
