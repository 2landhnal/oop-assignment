using UnityEngine.UI;
using UnityEngine;
using static AccountManager;
using System.Linq;
using TMPro;
using System;
using System.Collections.Generic;

public class RankingCard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameLabel, dataLabel;
    [SerializeField] protected Image avtIcon, iconImg;
    public GameObject border;
    public float maxSize;
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
        border.SetActive(userInfo.username == currentUsername);
    }

    public void ShowIcon(Sprite sprite)
    {
        iconImg.sprite = sprite;
        iconImg.SetNativeSize();
        float tmp = MathF.Min(maxSize / iconImg.rectTransform.rect.width, maxSize / iconImg.rectTransform.rect.height);
        iconImg.transform.localScale = new Vector2(tmp, tmp);
    }

    public void SetAmountTxt(int amount)
    {
        dataLabel.text = amount.ToString();
    }

    protected virtual void Trigger()
    {
        // show purchase and infor panel
    }
}
