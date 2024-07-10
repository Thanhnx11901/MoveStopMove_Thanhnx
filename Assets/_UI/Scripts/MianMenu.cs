using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MianMenu : UICanvas
{
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
