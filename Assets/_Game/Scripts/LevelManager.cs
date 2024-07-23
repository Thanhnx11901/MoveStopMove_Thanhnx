using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public Level currentLevel;  
    public PlayerCtl playerCtl;

    public List<Level> levels;

    private void Start() {
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
