using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public Level currentLevel;  
    public PlayerCtl playerCtl;

    public List<Level> levels;

    private void Start() {
         if (PlayerPrefs.GetInt("FirstTime", 0) == 0)
        {
            PlayerPrefs.SetString(Constants.WEAPON, "0");
            PlayerPrefs.SetInt(Constants.CURRENT_WEAPON,0);
            PlayerPrefs.SetInt(Constants.CURRENT_COIN, 500);
            PlayerPrefs.SetInt("FirstTime", 1);
            PlayerPrefs.Save();
        }
        currentLevel = Instantiate(levels[PlayerPrefs.GetInt(Constants.CURRENT_LEVEL)],transform);
        currentLevel.OnInit();
    }
    public void RetryLevel(){
        playerCtl.OnInit();
        currentLevel.OnInit();
    }
    public void NextLevel(){        
        playerCtl.OnInit();
        PlayerPrefs.SetInt(Constants.CURRENT_COIN,  PlayerPrefs.GetInt(Constants.CURRENT_COIN) + currentLevel.bonusCoin); 
        if(currentLevel != null) {
            currentLevel.DespawnAllBots();
            Destroy(currentLevel.gameObject);
        }
        PlayerPrefs.SetInt(Constants.CURRENT_LEVEL, PlayerPrefs.GetInt(Constants.CURRENT_LEVEL) +1);

        if(PlayerPrefs.GetInt(Constants.CURRENT_LEVEL) == levels.Count) {
            RetryLevel();
            UIManager.Ins.CloseAll();
            UIManager.Ins.OpenUI<MianMenu>();
            GameManager.ChangeState(GameState.MainMenu);
            return;
        }
        currentLevel = Instantiate(levels[PlayerPrefs.GetInt(Constants.CURRENT_LEVEL)],transform);
        currentLevel.OnInit();

        
    }
}
