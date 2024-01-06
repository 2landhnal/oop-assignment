using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{
    public List<GameObject> observes;
    private int enemyCounter, enemyDefeatCounter, coinCollected;
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
        coinCollected = 0;
        if(portal != null) portal.SetActive(false) ;
        if(treasure != null) treasure.SetActive(false);
        if(Player.instance == null) LoadPlayingGameData();
        foreach (GameObject observe in observes)
        {
            foreach(IObserve sub in observe.GetComponents<IObserve>())
            {
                sub.SubjectCalled();
            }
        }

        Invoke("SavePlayingGameData", 1f);
    }

    public void Lose()
    {
        SaveCoinCollected();
        SaveGemCollected();
        UIController.Ins.ShowResult();
        Destroy(Player.instance.gameObject);
    }

    void AllDefeated()
    {
        if (portal == null) return;
        portal.SetActive(true);
        if (treasure == null) return;
        treasure.SetActive(true);
        List<AccountManager.AccountGameData> tmpList = AccountManager.accountGameDataList;

        if (AccountManager.currentUsername == null) return;
        tmpList.Single(s => s.username == AccountManager.currentUsername).enemyKilledAmount += enemyDefeatCounter;
        FileHandler.SaveToJSON(tmpList, AccountManager.fileName_accountGameData);

        SaveCoinCollected();
    }

    public void LoadPlayingGameData()
    {
        if (AccountManager.currentUsername == null)
        {
            Instantiate(RuntimeData.Ins.characterInfoList[0].characterPrefab);
            return;
        }
        AccountManager.PlayingGameData tmpPlayingGameData = AccountManager.playingGameDataList.Single(s => s.username == AccountManager.currentUsername);

        GameObject player = Instantiate(RuntimeData.Ins.characterInfoList[tmpPlayingGameData.characterID].characterPrefab);

        player.GetComponent<HealthManager>().LoadHP(tmpPlayingGameData.maxHP, tmpPlayingGameData.currentHPRate);
        foreach(int id in tmpPlayingGameData.skillCollectedList)
        {
            player.GetComponent<SkillManager>().AddSkill(RuntimeData.Ins.skillControllerList[id].type);
        }

        player.GetComponent<Player>().gemCollected = tmpPlayingGameData.gemCollectedAmount;
        player.GetComponent<Player>().coinCollected = tmpPlayingGameData.coinAmount;
    }

    public void SavePlayingGameData()
    {
        if (AccountManager.currentUsername == null) return;
        List<AccountManager.PlayingGameData> playingList = AccountManager.playingGameDataList;
        List<AccountManager.AccountGameData> gameDataList = AccountManager.accountGameDataList;
        AccountManager.PlayingGameData tmpPlayingGameData = playingList.Single(s => s.username == AccountManager.currentUsername);
        AccountManager.AccountGameData tmpGameData = gameDataList.Single(s => s.username == AccountManager.currentUsername);

        int id = playingList.GetIndex(tmpPlayingGameData);

        tmpPlayingGameData.currentHPRate = Player.instance.GetComponent<HealthManager>().currentHPRate;
        tmpPlayingGameData.maxHP = Player.instance.GetComponent<HealthManager>().GetMaxHP();
        tmpPlayingGameData.sceneIndex = RuntimeData.Ins.sceneNameList.GetIndex(SceneManager.GetActiveScene().name);
        List<int> intList = new List<int>();
        foreach (SkillController tmp in Player.instance.GetComponent<SkillManager>().skillControllersCollected)
        {
            intList.Add(RuntimeData.Ins.skillControllerList.IndexOf(RuntimeData.Ins.skillControllerList.Single(s => s.skillData == tmp.skillData)));
        }
        tmpPlayingGameData.skillCollectedList = intList;

        // TODO
        tmpPlayingGameData.gemCollectedAmount = Player.instance.GetComponent<ResourceManager>().gemAmount;
        tmpPlayingGameData.coinAmount = Player.instance.GetComponent<ResourceManager>().coinAmount;

        playingList[id] = tmpPlayingGameData;

        gameDataList.Single(s => s.username == AccountManager.currentUsername).hasPlayingData = true;

        FileHandler.SaveToJSON(playingList, AccountManager.fileName_playingGameData);
        FileHandler.SaveToJSON(gameDataList, AccountManager.fileName_accountGameData);
    }

    void SaveGemCollected()
    {
        if (AccountManager.currentUsername == null) return;
        List<AccountManager.AccountGameData> gameDataList = AccountManager.accountGameDataList;
        AccountManager.AccountGameData tmpGameData = gameDataList.Single(s => s.username == AccountManager.currentUsername);

        int id = gameDataList.GetIndex(tmpGameData);
        tmpGameData.gemAmount += Player.instance.GetComponent<ResourceManager>().gemAmount;

        tmpGameData.hasPlayingData = false;

        gameDataList[id] = tmpGameData;

        FileHandler.SaveToJSON(gameDataList, AccountManager.fileName_accountGameData);
    }

    public void AddCoinCollected(int amount)
    {
        coinCollected += amount;
    }

    public void SaveCoinCollected()
    {
        if (AccountManager.currentUsername == null) return;
        List<AccountManager.AccountGameData> gameDataList = AccountManager.accountGameDataList;
        AccountManager.AccountGameData tmpGameData = gameDataList.Single(s => s.username == AccountManager.currentUsername);

        int id = gameDataList.GetIndex(tmpGameData);
        tmpGameData.coinCollectedAmount += coinCollected;

        gameDataList[id] = tmpGameData;

        FileHandler.SaveToJSON(gameDataList, AccountManager.fileName_accountGameData);
    }

}
