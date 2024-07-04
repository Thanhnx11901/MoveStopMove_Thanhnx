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
    [SerializeField]private Vector3 position;
    [SerializeField]private Vector3 rotation;

    [SerializeField] private float range;
    // tốc độ đánh 
    [SerializeField] private float attackSpeed;

    public GameObject imgShop;

    [SerializeField] private EWeapon nameWeapon;

    [SerializeField] private string addPower;

    private CharacterCtl owner;

    [SerializeField] private float timeDespawn;
    [SerializeField] private MeshRenderer meshRenderer;

    public CharacterCtl Owner { get => owner; set => owner = value; }
    public float TimeDespawn { get => timeDespawn; set => timeDespawn = value; }
    public float Range { get => range; set => range = value; }
    public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
    public EWeapon NameWeapon { get => nameWeapon;}
    public string AddPower { get => addPower;}
    public Vector3 Position { get => position;}
    public Vector3 Rotation { get => rotation;}

    public virtual void Fire(CharacterCtl enemy)
    {
        Debug.Log("fire");
    }

    public void ActiveMeshRenderer(bool isActive){
        if (meshRenderer == null) return;
        meshRenderer.enabled = isActive;
    }
}
