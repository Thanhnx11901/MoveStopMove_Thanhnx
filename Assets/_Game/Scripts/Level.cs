using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public List<Transform> listPointSpawnBots;

    public List<BotCtl> bots;
    public int totalBot;
    public int countBot;
    private float timer;
    private float countTime;
    private int countBotDie;

    public int bonusCoin;
    public void OnInit(){
        if(bots.Count > 0) DespawnAllBots();
        timer = 1.5f;
        countTime = 0f;
        countBot = 0;
        countBotDie = 0;
        SpawnBotInit();
    }
    public void SpawnBotInit()
    {
        for (int i = 0; i < listPointSpawnBots.Count; i++)
        {
            BotCtl bot = SimplePool.Spawn<BotCtl>(PoolType.Bot);
            bots.Add(bot);
            Vector2 randomPoint2D = Random.insideUnitCircle * 5;
            Vector3 randomPosition = new Vector3(randomPoint2D.x, 0, randomPoint2D.y) + listPointSpawnBots[i].position;
            bot.TF.position = randomPosition;
            countBot++;
        }
    }
    public void SpawnBot(Vector3 point)
    {
        countTime = 0f;
        BotCtl bot = SimplePool.Spawn<BotCtl>(PoolType.Bot);
        bots.Add(bot);
        Vector2 randomPoint2D = Random.insideUnitCircle * 5;
        Vector3 randomPosition = new Vector3(randomPoint2D.x, 0, randomPoint2D.y) + point;

        bot.TF.position = randomPosition;
        countBot++;
    }
    private void Update()
    {
        if (bots.Count < 15 && countBot <= totalBot)
        {
            if(countTime >= timer){
                int randomIndex = Random.Range(0, listPointSpawnBots.Count);
                SpawnBot(listPointSpawnBots[randomIndex].position);
            }
            countTime += Time.deltaTime;
        }
        if(countBotDie >= totalBot){
            UIManager.Ins.CloseAll();
            UIManager.Ins.OpenUI<Win>();
            GameManager.ChangeState(GameState.Victory);   
        }
    }
    public void removeBotWhenDead(BotCtl botCtl)
    {
        if (bots.Contains(botCtl))
        {
            bots.Remove(botCtl);
        }
        countBotDie++;
    }
    public void DespawnAllBots()
    {
        while (bots.Count > 0)
        {
            BotCtl bot = bots[0];
            bots.RemoveAt(0);
            SimplePool.Despawn(bot);
        }
        countBot = 0;
    }

    public int TotalBotAlive(){
        return totalBot - countBotDie;
    }
}
