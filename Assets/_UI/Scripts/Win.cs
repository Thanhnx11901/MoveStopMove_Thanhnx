using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Win : UICanvas
{
    public Text score;

    public void MainMenuButton()
    {
        LevelManager.Ins.RetryLevel();
        UIManager.Ins.OpenUI<MianMenu>();
        Close(0);
        GameManager.ChangeState(GameState.MainMenu);
    }
    public void NextLevel(){
        LevelManager.Ins.NextLevel();
        UIManager.Ins.OpenUI<MianMenu>();
        Close(0);
        GameManager.ChangeState(GameState.MainMenu);
    }
}
