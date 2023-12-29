using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    private int enemyCounter, enemyDefeatCounter;
    [SerializeField]GameObject portal, treasure;
    public void AddEnemyCounter()
    {
        enemyCounter++;
    }
    public void AddEnemyDefeatCounter()
    {
        enemyDefeatCounter++;
        if(enemyDefeatCounter == enemyCounter)
        {
            AllDefeated();
        }
    }
    protected override void Awake()
    {
        MakeSingleton(false);
    }
    void Start()
    {
        enemyCounter = 0;
        enemyDefeatCounter = 0;
        if(portal != null) portal.SetActive(false) ;
        if(treasure != null) treasure.SetActive(false);
    }

    void AllDefeated()
    {
        if (portal == null) return;
        portal.SetActive(true);
        if (treasure == null) return;
        treasure.SetActive(true);
        List<AccountManager.AccountGameData> tmpList = AccountManager.accountGameDataList;
        tmpList.Single(s => s.username == AccountManager.currentUsername).enemyKilledAmount += enemyDefeatCounter;
        FileHandler.SaveToJSON<AccountManager.AccountGameData>(tmpList, AccountManager.fileName_accountGameData);
    }

}
