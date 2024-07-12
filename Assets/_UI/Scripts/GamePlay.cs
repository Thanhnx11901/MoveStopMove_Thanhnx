using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlay : UICanvas
{
    public Text textAlive;
    public void SettingButton()
    {
        Time.timeScale = 0;
        UIManager.Ins.OpenUI<Setting>();
    }
    private void Update() {
        textAlive.text = "Alive: " + LevelManager.Ins.currentLevel.TotalBotAlive();
    }
}
