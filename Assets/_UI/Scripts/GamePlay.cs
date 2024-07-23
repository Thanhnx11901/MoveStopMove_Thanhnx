using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlay : UICanvas
{
    [SerializeField]private GameObject btnGuiobj;
    [SerializeField] private VariableJoystick Joystick;
    public Text textAlive;
    private void Start() {
        LevelManager.Ins.playerCtl.VariableJoystick = Joystick;
        btnGuiobj.SetActive(true);
    }
    public void SettingButton()
    {
        Time.timeScale = 0;
        UIManager.Ins.OpenUI<Setting>();
    }
    private void Update() {
        textAlive.text = "Alive: " + LevelManager.Ins.currentLevel.TotalBotAlive();
        if(Math.Abs(Joystick.Horizontal) > 0.00001f && Math.Abs(Joystick.Vertical) > 0.00001f){
            btnGuiobj.SetActive(false);
        }
    }
}
