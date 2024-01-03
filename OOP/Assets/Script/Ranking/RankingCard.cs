using UnityEngine.UI;
using UnityEngine;
using static AccountManager;
using System.Linq;
using TMPro;

public class RankingCard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameLabel, dataLabel;
    [SerializeField] protected Image avtIcon;

    protected AccountGameData accountGameData;

    public void Initialize(AccountGameData accountGameData)
    {
        this.accountGameData = accountGameData;
        UpdateUI();
    }

    protected virtual void UpdateUI()
    {
        if (accountGameData == null) return;
        if (!FileHandler.ReadListFromJSON<UserInfo>(fileName_userInfo).Any(s => s.username == accountGameData.username)) return;
        UserInfo userInfo = FileHandler.ReadListFromJSON<UserInfo>(fileName_userInfo).Single(s => s.username == accountGameData.username);
        avtIcon.sprite = RuntimeData.Ins.avtSprites[userInfo.avtSpriteId];
        nameLabel.text = userInfo.name;
        dataLabel.text = accountGameData.enemyKilledAmount.ToString();
    }

    protected virtual void Trigger()
    {
        // show purchase and infor panel
    }
}
