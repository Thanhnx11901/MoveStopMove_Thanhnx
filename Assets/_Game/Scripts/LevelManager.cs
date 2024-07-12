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
        currentLevel.OnInit(PlayerPrefs.GetInt(Constants.CURRENT_LEVEL)+1);
    }
    public void RetryLevel(){
        playerCtl.OnInit();
        currentLevel.OnInit(PlayerPrefs.GetInt(Constants.CURRENT_LEVEL)+1);
    }
    public void NextLevel(){
        playerCtl.OnInit();
        if(currentLevel != null) {
            currentLevel.DespawnAllBots();
            Destroy(currentLevel.gameObject);
        }
        PlayerPrefs.SetInt(Constants.CURRENT_LEVEL, PlayerPrefs.GetInt(Constants.CURRENT_LEVEL) +1);
        currentLevel = Instantiate(levels[PlayerPrefs.GetInt(Constants.CURRENT_LEVEL)],transform);
        currentLevel.OnInit(PlayerPrefs.GetInt(Constants.CURRENT_LEVEL)+1);
    }
}
