using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static AccountManager;

public class RankingSection : MonoBehaviour
{
    public RankingCard cardPrefab;
    RankingCard tmp;
    public Transform grid;
    List<AccountGameData> tmpGameDataList;
    public Sprite gem, coin, enemy, skill, boss, winGame;
    public void SortByEnemy()
    {
        Helper.ClearChilds(grid);
        tmpGameDataList = accountGameDataList.OrderBy(s=>s.enemyKilledAmount).ToList();
        tmpGameDataList.Reverse();
        //tmpGameDataList.Sort((x, y) => x.enemyKilledAmount.CompareTo(y.enemyKilledAmount));
        foreach (AccountGameData accountGameData in tmpGameDataList)
        {
            tmp = Instantiate(cardPrefab);
            tmp.Initialize(accountGameData);
            tmp.SetAmountTxt(accountGameData.enemyKilledAmount);
            Helper.AssignToRoot(grid, tmp.transform, Vector3.zero, Vector3.one);
        }
    }

    public void SortByGem()
    {
        Helper.ClearChilds(grid);
        tmpGameDataList = accountGameDataList.OrderBy(s => s.gemAmount).ToList();
        tmpGameDataList.Reverse();
        //tmpGameDataList.Sort((x, y) => x.enemyKilledAmount.CompareTo(y.enemyKilledAmount));
        foreach (AccountGameData accountGameData in tmpGameDataList)
        {
            tmp = Instantiate(cardPrefab);
            tmp.Initialize(accountGameData);
            tmp.ShowIcon(gem);
            tmp.SetAmountTxt(accountGameData.gemAmount);
            Helper.AssignToRoot(grid, tmp.transform, Vector3.zero, Vector3.one);
        }
    }

    public void SortByCoin()
    {
        Helper.ClearChilds(grid);
        tmpGameDataList = accountGameDataList.OrderBy(s => s.coinCollectedAmount).ToList();
        tmpGameDataList.Reverse();
        //tmpGameDataList.Sort((x, y) => x.enemyKilledAmount.CompareTo(y.enemyKilledAmount));
        foreach (AccountGameData accountGameData in tmpGameDataList)
        {
            tmp = Instantiate(cardPrefab);
            tmp.Initialize(accountGameData);
            tmp.ShowIcon(coin);
            tmp.SetAmountTxt(accountGameData.coinCollectedAmount);
            Helper.AssignToRoot(grid, tmp.transform, Vector3.zero, Vector3.one);
        }
    }

    public void SortByBoss()
    {
        Helper.ClearChilds(grid);
        tmpGameDataList = accountGameDataList.OrderBy(s => s.bossKilledAmount).ToList();
        tmpGameDataList.Reverse();
        //tmpGameDataList.Sort((x, y) => x.enemyKilledAmount.CompareTo(y.enemyKilledAmount));
        foreach (AccountGameData accountGameData in tmpGameDataList)
        {
            tmp = Instantiate(cardPrefab);
            tmp.Initialize(accountGameData);
            tmp.ShowIcon(boss);
            tmp.SetAmountTxt(accountGameData.bossKilledAmount);
            Helper.AssignToRoot(grid, tmp.transform, Vector3.zero, Vector3.one);
        }
    }

    public void SortBySkill()
    {
        Helper.ClearChilds(grid);
        tmpGameDataList = accountGameDataList.OrderBy(s => s.skillGainedIdList.Count).ToList();
        tmpGameDataList.Reverse();
        //tmpGameDataList.Sort((x, y) => x.enemyKilledAmount.CompareTo(y.enemyKilledAmount));
        foreach (AccountGameData accountGameData in tmpGameDataList)
        {
            tmp = Instantiate(cardPrefab);
            tmp.Initialize(accountGameData);
            tmp.ShowIcon(skill);
            tmp.SetAmountTxt(accountGameData.skillGainedIdList.Count);
            Helper.AssignToRoot(grid, tmp.transform, Vector3.zero, Vector3.one);
        }
    }

    public void SortByGame()
    {
        Helper.ClearChilds(grid);
        tmpGameDataList = accountGameDataList.OrderBy(s => s.gameCompletedAmount).ToList();
        tmpGameDataList.Reverse();
        //tmpGameDataList.Sort((x, y) => x.enemyKilledAmount.CompareTo(y.enemyKilledAmount));
        foreach (AccountGameData accountGameData in tmpGameDataList)
        {
            tmp = Instantiate(cardPrefab);
            tmp.Initialize(accountGameData);
            tmp.ShowIcon(winGame);
            tmp.SetAmountTxt(accountGameData.gameCompletedAmount);
            Helper.AssignToRoot(grid, tmp.transform, Vector3.zero, Vector3.one);
        }
    }
}
