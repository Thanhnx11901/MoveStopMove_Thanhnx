using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShop : UICanvas
{
    private PlayerCtl playerCtl;
    [SerializeField] private WeaponConfig weaponConfig;
    private List<Weapon> weapons;
    [SerializeField] private Text textNameWeapon;
    [SerializeField] private Text textAddPower;
    [SerializeField] private Text textMoney;

    [SerializeField] private GameObject panelWeapon;

    [SerializeField] private GameObject equippedButton;

    [SerializeField] private GameObject selectButton;

    [SerializeField] private GameObject buyButton;

    private EWeapon eCurrentWeapon;

    private Weapon currentWeapon;
    private void OnEnable()
    {
        playerCtl = LevelManager.Ins.playerCtl;
        playerCtl.gameObject.SetActive(false);
        eCurrentWeapon = EWeapon.Hammer;
        UpdateUI();
    }
    public void NextWeapon()
    {
        eCurrentWeapon = (EWeapon)(((int)eCurrentWeapon + 1) % System.Enum.GetValues(typeof(EWeapon)).Length);
        UpdateUI();
    }
    public void PreviousWeapon()
    {
        eCurrentWeapon = (EWeapon)(((int)eCurrentWeapon - 1 + System.Enum.GetValues(typeof(EWeapon)).Length) % System.Enum.GetValues(typeof(EWeapon)).Length);
        UpdateUI();
    }
    private void UpdateUI()
    {
        if (currentWeapon != null) Destroy(currentWeapon.gameObject);

        currentWeapon = Instantiate(weaponConfig.GetWeapon(eCurrentWeapon), panelWeapon.transform);
        textNameWeapon.text = currentWeapon.NameWeapon.ToString();
        textAddPower.text = currentWeapon.AddPower;


        if (PlayerPrefs.GetInt(currentWeapon.NameWeapon.ToString()) == 0)
        {
            buyButton.SetActive(true);
            equippedButton.SetActive(false);
            selectButton.SetActive(false);
        }
        else if (playerCtl.ECurrentWeapon == eCurrentWeapon)
        {
            equippedButton.SetActive(true);
            buyButton.SetActive(false);
            selectButton.SetActive(false);
        }
        else
        {
            equippedButton.SetActive(false);
            buyButton.SetActive(false);
            selectButton.SetActive(true);
        }
    }

    public void SelectButton()
    {
        playerCtl.WeaponHolder.ChangeWeapon(eCurrentWeapon);
        ExitButton();
    }
    public void EquippedButton()
    {
        playerCtl.WeaponHolder.ChangeWeapon(eCurrentWeapon);
        ExitButton();
    }
    public void BuyButton()
    {
        PlayerPrefs.SetInt(currentWeapon.NameWeapon.ToString(), 1);
        UpdateUI();
    }

    public void ExitButton()
    {
        playerCtl.gameObject.SetActive(true);
        UIManager.Ins.OpenUI<MianMenu>();
        Close(0);
    }
}
