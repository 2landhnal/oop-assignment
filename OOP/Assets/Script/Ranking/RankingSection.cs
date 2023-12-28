using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AccountManager;

public class RankingSection : MonoBehaviour
{
    List<AccountGameData> accountGameDataList;
    public RankingCard cardPrefab;
    RankingCard tmp;
    public Transform grid;
    public void ShowRanking()
    {
        Helper.ClearChilds(grid);
        accountGameDataList = FileHandler.ReadListFromJSON<AccountGameData>(fileName_accountGameData);
        foreach(AccountGameData accountGameData in accountGameDataList)
        {
            tmp = Instantiate(cardPrefab);
            cardPrefab.Initialize(accountGameData);
            Helper.AssignToRoot(grid, tmp.transform, Vector3.zero, Vector3.one);
        }
    }
}
