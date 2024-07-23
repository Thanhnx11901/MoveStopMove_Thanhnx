using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MianMenu : UICanvas
{
    public Text txtCurrentLevel;

    public Text txtCoin;

    private void OnEnable() {
        txtCurrentLevel.text = "Level: "+ (PlayerPrefs.GetInt(Constants.CURRENT_LEVEL)+1);
        txtCoin.text = PlayerPrefs.GetInt(Constants.CURRENT_COIN).ToString();
    }
    public void PlayButton()
    {
        GameManager.ChangeState(GameState.GamePlay);
        UIManager.Ins.OpenUI<GamePlay>();
        Close(0);
    }
    public void WeaponButton()
    {
        UIManager.Ins.OpenUI<WeaponShop>();
        Close(0);
    } 
    public void SkinButton()
    {
        GameManager.ChangeState(GameState.ShopSkin);
        LevelManager.Ins.playerCtl.ChangeSkinPlayer.ChangeSkinDefault();
        UIManager.Ins.OpenUI<SkinShop>();
        Close(0);
    } 
}
