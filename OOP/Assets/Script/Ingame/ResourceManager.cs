using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using static AccountManager;

public class ResourceManager : MonoBehaviour
{
    [HideInInspector]public int coinAmount, gemAmount;
    PlayingGameData currentPlayingGameData;
    int id;
    private void Start()
    {
        currentPlayingGameData = GetCurrentPlayingGameData();
        id = playingGameDataList.IndexOf(currentPlayingGameData);
        coinAmount = currentPlayingGameData.coinAmount;
        gemAmount = currentPlayingGameData.gemCollectedAmount;
    }

    public void AddCoin(int amount)
    {
        coinAmount += amount;
        GameController.Ins.AddCoinCollected(amount);
    }
    public void UseCoin(int amount)
    {
        if (amount > coinAmount) return;
        coinAmount -= amount;
    }
    public void AddGem(int amount)
    {
        gemAmount += amount;
    }
    public void UseGem(int amount)
    {
        if (amount > gemAmount) return;
        gemAmount -= amount;
    }

    public void SaveData()
    {
        currentPlayingGameData.coinAmount = coinAmount;
        currentPlayingGameData.gemCollectedAmount = gemAmount;
        List<PlayingGameData> tmpList = playingGameDataList;
        tmpList[id] = currentPlayingGameData;
        FileHandler.SaveToJSON(tmpList, fileName_playingGameData);
    }
}
