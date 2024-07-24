using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private CharacterCtl owner;
    // xóa tầm đánh và tốc độ đánh của vũ khí trước
    public void DelWeapon(){
        Destroy(owner.CurrentWeapon.gameObject);
    }
    public void ChangeWeapon(EWeapon eWeapon){
        if(owner.CurrentWeapon != null) DelWeapon();
        //lưu vũ khí
        PlayerPrefs.SetInt(Constants.CURRENT_WEAPON, (int)eWeapon);
        //sinh vũ khí
        Weapon weapon = Instantiate(GameData.Ins.weaponConfig.GetWeapon(eWeapon), transform);
        weapon.Owner = owner;
        owner.CurrentWeapon = weapon;
        owner.ECurrentWeapon = eWeapon;
        weapon.TF.localPosition = weapon.Position;
        weapon.TF.localRotation = Quaternion.Euler(weapon.Rotation);

        owner.AddAttackRange(weapon.Range);
        owner.AddAttackSpeed(weapon.AttackSpeed);
    }
    public void ChangeWeaponBot(EWeapon eWeapon){
        if(owner.CurrentWeapon != null) DelWeapon();
        //sinh vũ khí
        Weapon weapon = Instantiate(GameData.Ins.weaponConfig.GetWeapon(eWeapon), transform);
        weapon.Owner = owner;
        owner.CurrentWeapon = weapon;
        owner.ECurrentWeapon = eWeapon;
        weapon.TF.localPosition = weapon.Position;
        weapon.TF.localRotation = Quaternion.Euler(weapon.Rotation);

        // thêm tầm đánh hoặc tốc độ đánh 
        owner.AddAttackRange(weapon.Range);
        owner.AddAttackSpeed(weapon.AttackSpeed);
    }
}
