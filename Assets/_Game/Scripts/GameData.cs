using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : Singleton<GameData>
{
    public CharacterConfig characterConfig;
    public HairConfig hairConfig;
    public PantConfig pantConfig;
    public ShieldConfig shieldConfig;
    public WeaponConfig weaponConfig;
}
