using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        Invoke("SavePlayingGameData", 1f);
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

    public void SavePlayingGameData()
    {
        if (AccountManager.currentUsername == null) return;
        List<AccountManager.PlayingGameData> playingList = AccountManager.playingGameDataList;
        List<AccountManager.AccountGameData> gameDataList = AccountManager.accountGameDataList;
        AccountManager.PlayingGameData tmpPlayingGameData = playingList.Single(s=>s.username == AccountManager.currentUsername);
        AccountManager.AccountGameData tmpGameData = gameDataList.Single(s => s.username == AccountManager.currentUsername);

        int id = playingList.GetIndex(tmpPlayingGameData);

        tmpPlayingGameData.characterID = RuntimeData.Ins.characterInfoList.IndexOf(RuntimeData.Ins.characterInfoList.Single(s => s.characterPrefab.name == Player.instance.name));
        tmpPlayingGameData.currentHPRate = Player.instance.GetComponent<HealthManager>().currentHPRate;
        tmpPlayingGameData.sceneIndex = RuntimeData.Ins.sceneNameList.GetIndex(SceneManager.GetActiveScene().name);
        List<int> intList = new List<int>();
        foreach(SkillController tmp in Player.instance.GetComponent<SkillManager>().skillControllersCollected)
        {
            intList.Add(RuntimeData.Ins.skillControllerList.IndexOf(RuntimeData.Ins.skillControllerList.Single(s=>s.skillData == tmp.skillData)));
        }
        tmpPlayingGameData.skillCollectedList = intList;

        // TODO
        tmpPlayingGameData.gemCollectedAmount = 0;
        tmpPlayingGameData.coinAmount = 0;

        playingList[id] = tmpPlayingGameData;

        gameDataList.Single(s => s.username == AccountManager.currentUsername).hasPlayingData = true;

        FileHandler.SaveToJSON(playingList, AccountManager.fileName_playingGameData);
        FileHandler.SaveToJSON(gameDataList, AccountManager.fileName_accountGameData);
    }

}
