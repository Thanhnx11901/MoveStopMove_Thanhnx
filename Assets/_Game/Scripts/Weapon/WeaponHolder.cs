using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private CharacterCtl owner;
    [SerializeField] private WeaponConfig weaponConfig;
    public Weapon weaponPrefab;
    private void Awake()
    {
        PlayerPrefs.SetInt(Constants.CURRENT_WEAPON, 0);
    }
    private void Start()
    {
        int indexWeapon = PlayerPrefs.GetInt(Constants.CURRENT_WEAPON);
        weaponPrefab = weaponConfig.GetWeapon(indexWeapon);

    }
    public void SpawnWeapon()
    {
        Weapon weapon = Instantiate(weaponPrefab, transform);
        weapon.Owner = owner;
        owner.currentWeapon = weapon;

        // weapon.TF.localPosition = new Vector3(-0.45f, 0.389f, 0.042f);
        // weapon.TF.localRotation = Quaternion.Euler(0, 0, 78);


        // hammer

        //set vị trí
        weapon.TF.localPosition = new Vector3(-0.1f, 0.45f, 0.05f);
        weapon.TF.localRotation = Quaternion.Euler(165, 0, -50);
        owner.AddAttackRange(weapon.Range);
        owner.AddAttackSpeed(weapon.AttackSpeed);

        //set 
        // tốc độ đánh

        // tầm đánh 


    }

}
