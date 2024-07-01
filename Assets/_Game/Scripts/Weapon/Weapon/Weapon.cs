using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SkinCharacter
{
    None = 0,
    Chambi = 1,
    Batman = 2,
}
public enum Hair
{
    None = 0,

    Arrow = 1,
    Cowboy = 2,

    Crown = 3,
    Ear = 4,
    Hat = 5,
    Hat_Cap = 6,
    Hat_Yellow = 7,
    HeadPhone = 8,
    Horm = 9,
    Rau = 10,
}
public enum Shield
{
    None = 0,
    Khien = 1,
    Shield = 2
}
public class Weapon : GameUnit
{

    [SerializeField] private float range;
    // tốc độ đánh 
    [SerializeField] private float attackSpeed;

    private CharacterCtl owner;

    private float timeDespawn;

    private void Awake() {
        TimeDespawn = 1f;
    }

    [SerializeField] private MeshRenderer meshRenderer;

    public CharacterCtl Owner { get => owner; set => owner = value; }
    public float TimeDespawn { get => timeDespawn; set => timeDespawn = value; }
    public float Range { get => range; set => range = value; }
    public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
    public virtual void Fire(CharacterCtl enemy)
    {
        Debug.Log("fire");
    }

    public void ActiveMeshRenderer(bool isActive){
        meshRenderer.enabled = isActive;
    }
}
