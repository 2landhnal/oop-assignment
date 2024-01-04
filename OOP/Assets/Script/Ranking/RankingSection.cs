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
    public void ShowRanking()
    {
        Helper.ClearChilds(grid);
        tmpGameDataList = accountGameDataList.OrderBy(s=>s.enemyKilledAmount).ToList();
        tmpGameDataList.Reverse();
        //tmpGameDataList.Sort((x, y) => x.enemyKilledAmount.CompareTo(y.enemyKilledAmount));
        foreach (AccountGameData accountGameData in tmpGameDataList)
        {
            tmp = Instantiate(cardPrefab);
            tmp.Initialize(accountGameData);
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
            tmp.ShowGem();
            Helper.AssignToRoot(grid, tmp.transform, Vector3.zero, Vector3.one);
        }
    }
}
