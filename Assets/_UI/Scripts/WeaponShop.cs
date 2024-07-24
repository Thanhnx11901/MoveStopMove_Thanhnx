using UnityEngine;
using UnityEngine.UI;

public class WeaponShop : UICanvas
{
    public Text txtCoin;
    private PlayerCtl playerCtl;
    [SerializeField] private WeaponConfig weaponConfig;
    [SerializeField] private Text textNameWeapon;
    [SerializeField] private Text textAddPower;
    [SerializeField] private Text textMoney;

    [SerializeField] private GameObject panelWeapon;

    [SerializeField] private GameObject equippedButton;

    [SerializeField] private GameObject selectButton;

    [SerializeField] private GameObject buyButton;

    [SerializeField] private Image image;

    private EWeapon eCurrentWeapon;

    private Weapon currentWeapon;
    private void Start()
    {
        if (PlayerPrefs.GetInt("FirstTime1", 0) == 0)
        {
            eCurrentWeapon = EWeapon.Hammer;
            PlayerPrefs.SetInt("FirstTime1", 1);
        }
    }
    private void OnEnable()
    {
        playerCtl = LevelManager.Ins.playerCtl;
        playerCtl.gameObject.SetActive(false);
        txtCoin.text = PlayerPrefs.GetInt(Constants.CURRENT_COIN).ToString();
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
        currentWeapon = weaponConfig.GetWeapon(eCurrentWeapon);




        ItemData itemDataWeapon = GameData.Ins.itemDataConfig.GetItemData(ESkin.Weapon, (int)eCurrentWeapon);

        int coin = PlayerPrefs.GetInt(Constants.CURRENT_COIN);
        txtCoin.text = coin.ToString();
        if (coin < itemDataWeapon.Price)
        {
            textMoney.color = Color.red;
        }
        else
        {
            textMoney.color = Color.black;

        }
        image.sprite = itemDataWeapon.icon;

        textNameWeapon.text = itemDataWeapon.Name;
        textAddPower.text = currentWeapon.AddPower;
        textMoney.text = itemDataWeapon.Price.ToString();

        string data = PlayerPrefs.GetString(Constants.WEAPON);
        int id = (int)eCurrentWeapon;
        if (data.Contains(id.ToString()))
        {
            buyButton.SetActive(false);
            equippedButton.SetActive(false);
            selectButton.SetActive(true);
        }
        else
        {
            buyButton.SetActive(true);
            equippedButton.SetActive(false);
            selectButton.SetActive(false);
        }
        if (PlayerPrefs.GetInt(Constants.CURRENT_WEAPON) == (int)eCurrentWeapon)
        {
            buyButton.SetActive(false);
            equippedButton.SetActive(true);
            selectButton.SetActive(false);
        }

    }

    public void SelectButton()
    {
        PlayerPrefs.SetInt(Constants.CURRENT_WEAPON, (int)eCurrentWeapon);
        playerCtl.OnInit();
        ExitButton();
    }
    public void EquippedButton()
    {
        PlayerPrefs.SetInt(Constants.CURRENT_WEAPON, (int)eCurrentWeapon);
        playerCtl.OnInit();
        ExitButton();
    }
    public void BuyButton()
    {
        int coin = PlayerPrefs.GetInt(Constants.CURRENT_COIN);
        if (coin < GameData.Ins.itemDataConfig.GetItemData(ESkin.Weapon, (int)eCurrentWeapon).Price) return;
        coin = coin - GameData.Ins.itemDataConfig.GetItemData(ESkin.Weapon, (int)eCurrentWeapon).Price;
        PlayerPrefs.SetInt(Constants.CURRENT_COIN, coin);

        string data = PlayerPrefs.GetString(Constants.WEAPON).ToString() + (int)eCurrentWeapon;
        PlayerPrefs.SetString(Constants.WEAPON, data);
        Debug.Log(PlayerPrefs.GetString(Constants.WEAPON));
        UpdateUI();
    }

    public void ExitButton()
    {
        playerCtl.gameObject.SetActive(true);
        UIManager.Ins.OpenUI<MianMenu>();
        Close(0);
    }
}
