using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : UICanvas
{
    public void ContinueButton()
    {
        Time.timeScale = 1f;
        Close(0);
    }
    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        LevelManager.Ins.RetryLevel();
        UIManager.Ins.CloseAll();
        UIManager.Ins.OpenUI<MianMenu>();
        GameManager.ChangeState(GameState.MainMenu);
    }
}
