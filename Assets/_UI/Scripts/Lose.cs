using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lose : UICanvas
{
    public Text textAlice;
    private void OnEnable() {
        textAlice.text = "#"+LevelManager.Ins.currentLevel.TotalBotAlive();
    }

    public void MainMenuButton()
    {
        LevelManager.Ins.RetryLevel();
        UIManager.Ins.OpenUI<MianMenu>();
        Close(0);
        GameManager.ChangeState(GameState.MainMenu);
    }
    public void RetryButton()
    {
        LevelManager.Ins.RetryLevel();
        UIManager.Ins.OpenUI<GamePlay>();
        Close(0);
        GameManager.ChangeState(GameState.GamePlay);

    }
}
